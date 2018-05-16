using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using lib;
using lib.Interfaces;

namespace QuIsec_Server
{
    public class FileTransferServer : Common, IGameViewController

    {
        public ControlPanelController _parent { get; set; }
        private TcpListener _serverSocket;
        public List<TrataCliente> Clientes;
        public Mutex ClientesMutex;
        private Thread _thread;

        public FileTransferServer(ControlPanelController parente)
        {
            _parent = parente;
            ClientesMutex = new Mutex();
            Clientes = new List<TrataCliente>();
            //var iphostEntry = Dns.GetHostEntry(Dns.GetHostName());
            _serverSocket = new TcpListener(IPAddress.Parse("127.0.0.1"), Port);


        }

        public void Run()
        {
            try
            {
                _serverSocket.Start();
                while (true)
                {
                    var handler = _serverSocket.AcceptSocket();
                    ClientesMutex.WaitOne();
                    Clientes.Add(new TrataCliente(handler, this,_parent));
                    var th = new Thread(Clientes[Clientes.Count - 1].Run);
                    ClientesMutex.ReleaseMutex();
                    th.Start();
                }
            }
            catch (Exception e)
            {
                Console.Out.Write(e);
            }
        }


        public void Start()
        {
            _thread = new Thread(Run);
            _thread.Start();
        }

        public void Stop()
        {
            try
            {
                if (_thread != null)
                {
                    _serverSocket.Stop();
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
            }
        }

        public void RemoveTrataCliente(TrataCliente remove)
        {
            ClientesMutex.WaitOne();
            Clientes.Remove(remove);
            ClientesMutex.ReleaseMutex();
        }

        public void SetQuest(Question quest)
        {
            foreach (var cliente in Clientes)
            {
                cliente.SetQuest(quest);
            }
        }

        public bool End(bool fromView = false)
        {
            foreach (var cliente in Clientes)
            {
                cliente.End(fromView);
            }

            return true;
        }

        public void ChangedTeamInformation(Team[] teams)
        {
            foreach (var cliente in Clientes)
            {
                cliente.ChangedTeamInformation(teams);
            }
        }

        public void SetTime(int remainTime)
        {
            foreach (var cliente in Clientes)
            {
                cliente.SetTime(remainTime);
            }
        }

        public void ShowRightAnswer(string currentQuestRightAnswer)
        {
            foreach (var cliente in Clientes)
            {
                cliente.ShowRightAnswer(currentQuestRightAnswer);
            }
        }
    }
}