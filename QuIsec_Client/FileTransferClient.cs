using System;
using System.Net.Sockets;
using System.Threading;
using lib;
using lib.Interfaces;

namespace QuIsec_Client
{
    class FileTransferClient : Common, IControlPanelController
    {
        private TcpClient _socket;
        public string ip { get; set; } = "127.0.0.1";
        private Thread _receiverThread;
        private bool _continue = true;
        private IGameViewController _parent;

        public FileTransferClient(IGameViewController parent)
        {
            _parent = parent;
        }

        public void Start()
        {
            try
            {
                _socket = new TcpClient();
                _socket.Connect(ip, Port);
                _receiverThread = new Thread(RunReceiver);
                _receiverThread.Start();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                Stop();
            }
        }

        private void RunReceiver()
        {
            try
            {
                var stream = _socket.GetStream();
                while (_continue)
                {
                    try
                    {
                        var request = ReceiveObject<Request>(stream);

                        Console.Out.WriteLine(request);
                        if (request == null)
                            break;

                        switch (request.Type)
                        {
                            case RequestType.Time:
                                _parent.SetTime((int) request.obj);
                                break;
                            case RequestType.ShowRightAnswer:
                                _parent.ShowRightAnswer(request.Msg);
                                break;
                            case RequestType.Teams:
                                _parent.ChangedTeamInformation((Team[]) request.obj);
                                break;
                            case RequestType.Quest:
                                _parent.SetQuest((Question) request.obj);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Stop();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
            }
        }

        public void Stop()
        {
            _continue = false;
            _socket?.Close();
            Console.In.Read();
        }
    }
}