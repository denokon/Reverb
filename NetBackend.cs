using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Reverb
{
    //A bejövő csomagok kezelő objektuma, copyztam valszeg teljesen át kell írni
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BUFFER_SIZE = 1024;
        public byte[] buffer = new byte[BUFFER_SIZE];
        public StringBuilder sb = new StringBuilder();
    }

    //Kapcsolat object, ez áll a legtávolabb a készenléttől
    class Peer
    {
        #region Változók
        bool Disconnecting = false, receiving = false;
        TcpClient _client;
        AutoResetEvent BlockTillReceive = new AutoResetEvent(false);
        Queue<StateObject> packetQueue = new Queue<StateObject>();
        const char EOT = '\u0004'; //End-of-transmission karakter
        #endregion

        //Konstruktor
        public Peer(TcpClient _client)
        {
            this._client = _client;
        }

        //Beérkező packetek után hallgatózó method. Ez fut egészen a peer szétkapcsolásáig, hiányosabb mint a nudisták úszóruházata
        public void Listen()
        {
            //Változók
            NetworkStream _stream = _client.GetStream();
            StateObject _so = new StateObject();
            EndPoint _endpoint = _client.Client.RemoteEndPoint;
            Socket _socket = _client.Client;
            byte[] StreamBuffer = new byte[1024];


            //Főciklus
            while (!Disconnecting)
            {
                try {
                /*_socket.Receive(buffer);*/
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode == SocketError.TimedOut)
                    {
                        //Csinálj valamit
                    }
                }

                if (Disconnecting) break;

                if (StreamBuffer[StreamBuffer.Length-1] != EOT)
                
                if (_socket.Available > 0)
                {
                    receiving = true;
                    _socket.ReceiveTimeout = 20000;
                }

                if (receiving)
                {
                    if (_socket.Available == 0)
                    {
                        //blabla feldolgozás
                        receiving = false;
                        /*buffer = null;*/
                    }
                }

                BlockTillReceive.Set();
            }


            //Vége a hallgatózásnak, szétkapcsolás
            _client.Client.Shutdown(SocketShutdown.Send);
            _client.GetStream().Close();
            _client.Close();
        }

        //SZAR
        /*public string SendAndWait(params object blabla)
        {
            bool whatwewant = false;
            //Send(blabla);
            
            do
            {
                BlockTillReceive.WaitOne();
                int i = -1;
                lock (packetQueue)
                {
                    if (packetQueue.Peek().packetType == PacketType.amitakarunk)
                        whatwewant = true;
                }
            } while (!whatwewant);
            return null;
        }*/

        //Üzenet küldése peer felé
        public void Send()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            Disconnecting = true;
        }
    }

    //Peer (szét)kapcsolódás eventjének objektumai
    #region PEER EVENT OBJEKTUMOK
    public enum ClientActionType { Connect, Disconnect }

    public class PeerEventArgs
    {
        public IPAddress address;
        public ClientActionType action;

        public PeerEventArgs(IPAddress address, ClientActionType action)
        {
            this.address = address;
            this.action = action;
        }
    }
    #endregion

    //Maga a netkód
    class NetBackend
    {

        #region VÁLTOZÓK
        //Private
        AutoResetEvent connectionWaitHandle = new AutoResetEvent(false);
        List<Peer> Peers = new List<Peer>();
        bool ServerShutdown = false;
        const int ClientPort = 20100;
        
        //Public
        public Exception _Exception { get; private set; }
        public IPAddress MyIP { get; private set; }
        #endregion

        #region EVENTEK
        public delegate void PeerEventHandler(object sender, PeerEventArgs data);

        public event PeerEventHandler PeerEvent;

        protected void OnPeerEvent(object sender, PeerEventArgs data)
        {
            if (PeerEvent != null)
            {
                PeerEvent(sender, data);
            }
        }
        #endregion

        //Konstruktor
        public NetBackend()
        {
            return;
            //A felénk érkező kapcsolatok után hallgatózó Thread elindítása
            Thread _ListenerThread = new Thread(ListenerThread);
            _ListenerThread.Start();
        }

        /*
         * PRIVATE METHODOK
         */

        //Szerveroldal főthread, a program leállításáig fut. Hallgatózik hozzánk kapcsolódók után és minden beérkező kapcsolat után nyit annak a peernek egy külön threadet
        void ListenerThread()
        {
            //Hallgatózás indítása minden IP felől
            TcpListener _Listener = new TcpListener(IPAddress.Any, 20100);
            _Listener.Start();

            //Beérkező kapcsolatokat fogadó ciklus
            while (true)
            {
                _Listener.BeginAcceptTcpClient(IncomingConnection,_Listener); //Ez elindítja a peer threadjét
                connectionWaitHandle.WaitOne(); //Ez blokkol, amíg a peer threadje nem jelzi a beérkező kapcsolódás befejeztét (program leállásakor is feloldjuk, hogy a következő sor leállítsa a futást)
                if (ServerShutdown) break;
            }

            //Hallgatózás leáll
            _Listener.Stop();
        }

        //Beérkező kapcsolatokat kezelő Thread methodja
        void IncomingConnection(IAsyncResult result)
        {
            //Beérkező kapcsolat nyugtázása és TcpClient kinyerése
            TcpClient client = ((TcpListener)result.AsyncState).EndAcceptTcpClient(result);
            
            //Event ellövése mert kapcsolat létrejött
            OnPeerEvent(client, new PeerEventArgs(IPAddress.Parse(((IPEndPoint)client.Client.RemoteEndPoint).ToString()), ClientActionType.Connect)); //csináld meg
            connectionWaitHandle.Set(); //Jelzés a fő Listener threadnek, hogy mehet a következő beérkező után a hallgatózás
            Peer peer = new Peer(client); //Új objektumot hozunk létre a peer számára..
            Peers.Add(peer); //..és hozzáadjuk a listához
            
            peer.Listen(); //Átadjuk az irányítást a Peer objektum hallgatózó metódusának, ennek csak akkor lesz vége ha szétkapcsolt

            Peers.Remove(peer); //Peer eltávolítása a listából
            OnPeerEvent(client, new PeerEventArgs(IPAddress.Loopback, ClientActionType.Disconnect)); //Event ellövése mert szétkapcsolt egy kapcsolat
        }

        /*
         * PUBLIC METHODOK
         */

        //Kapcsolódás a megadott IPhez
        public void ConnectPeer(IPAddress Address)
        {
            throw new NotImplementedException();
        }

        //Megadott IP szétkapcsolása
        public void DisconnectPeer(IPAddress address)
        {
            int num = Peers.Count, i = -1;
            lock (Peers)
            {
                while (++i < num && address != IPAddress.Parse(Peers[i].ToString()));
            }

            if (i < num) Peers[i].Disconnect();
        }

        //A form bezárásakor (szval a program leállásakor) fut le
        public void Shutdown()
        {
            ServerShutdown = true;
            connectionWaitHandle.Set();

            int num = Peers.Count;
            //Végigiterálunk az összes kliensen és meghívjuk a szétkapcsoló metódusukat
            for (int i = 0; i < num; ++i)
            {
                Peers[i].Disconnect();
            }
        }
    }
}
