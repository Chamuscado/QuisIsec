using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using lib;
using lib.Interfaces;

namespace QuIsec_Server
{
    public class TrataCliente : Common, IGameViewController
    {
        private Socket _socket;
        private ControlPanelController _parent;
        private NetworkStream _stream;
        private FileTransferServer fileTransferServer;

        public TrataCliente(Socket socket, FileTransferServer fileTransferServer, ControlPanelController parent)
        {
            this._socket = socket;
            this.fileTransferServer = fileTransferServer;
            _parent = parent;
        }

        public void Run()
        {
            _stream = new NetworkStream(_socket);
            var cont = true;
            while (cont)
            {
                try
                {
                    var request = ReceiveObject<Request>(_stream);

                    switch (request.Type)
                    {
                        case RequestType.End:
                            cont = false;
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.Out.WriteLine($"Cliente Desconectado: {_socket.RemoteEndPoint}");
                    break;
                }
            }

            fileTransferServer.RemoveTrataCliente(this);
        }

        public void Send(Request request)
        {
            try
            {
                SendObject(_stream, request);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine($" <{_socket.RemoteEndPoint}>" + e);
                _socket.Close();
            }
        }

        public void SetQuest(Question quest)
        {
            Send(new Request()
            {
                Type = RequestType.Quest,
                obj = quest
            });
        }

        public bool End(bool fromView = false)
        {
            Send(new Request()
            {
                Type = RequestType.End
            });
            return true;
        }

        public void ChangedTeamInformation(Team[] teams)
        {
            Send(new Request()
            {
                Type = RequestType.Teams,
                obj = teams
            });
        }

        public void SetTime(int remainTime)
        {
            Send(new Request()
            {
                Type = RequestType.Time,
                obj = remainTime
            });
        }

        public void ShowRightAnswer(string currentQuestRightAnswer)
        {
            Send(new Request()
            {
                Type = RequestType.ShowRightAnswer,
                Msg = currentQuestRightAnswer
            });
        }
    }
}