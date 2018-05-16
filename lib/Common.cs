using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace lib
{
    public abstract class Common
    {
        protected const int Port = 9000;


        protected T ReceiveObject<T>(NetworkStream stream)
        {
            int bytesRec;
            var @int = new byte[sizeof(Int32)];
            stream.Read(@int, 0, @int.Length);
            var total = BitConverter.ToInt32(@int, 0);

            byte[] bytes = new byte[total];
            //do
            //{
            bytesRec = stream.Read(bytes, 0, bytes.Length);

            //} while (bytesRec == bytes.Length);

            Console.Out.WriteLine("Recevidos : " + bytesRec + " bytes " + total + " bytes");
            return FromByteArray<T>(bytes);
        }


        protected void SendObject<T>(NetworkStream stream, T obj)
        {
            byte[] bytes = ToByteArray(obj);

            byte[] @int = BitConverter.GetBytes(bytes.Length);
            stream.Write(@int, 0, @int.Length);

            var bytesSend = 0;
            do
            {
                var remain = bytes.Length - bytesSend;

                stream.Write(bytes, bytesSend, bytesSend + (remain < 1024 ? remain : 1024));

                bytesSend += (remain < 1024 ? remain : 1024);

            } while (bytes.Length < bytesSend);

            Console.Out.WriteLine("Enviados : " + bytesSend + " bytes");
        }



        protected void ReceiveFile(Request request, NetworkStream networkStream)
        {
            try
            {
                var fileInfo = ReceiveObject<FileInfo>(networkStream);

                var step = fileInfo.Length / 1024 / 10;

                using (var file = new FileStream(Environment.CurrentDirectory + "\\" + Path.GetFileName(request.Msg),
                    FileMode.Create))
                {
                    int total = 0;
                    int i = 0;
                    var bytes = new byte[1024];

                    int bytesRec;
                    do
                    {
                        bytesRec = networkStream.Read(bytes, 0, bytes.Length);
                        file.Write(bytes, 0, bytesRec);
                        total += bytesRec;
                        i++;
                        if (i % step == 0)
                            Console.Out.WriteLine(((double)total / fileInfo.Length * 100).ToString("##") + " %");
                    } while (bytesRec == bytes.Length);

                    file.Flush();
                    Console.Out.WriteLine("Fim da transferencia do ficheiro" + Path.GetFileName(request.Msg));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        private byte[] ToByteArray<T>(T obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                return (T)bf.Deserialize(ms);
            }
        }
    }
}