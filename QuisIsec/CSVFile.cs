using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lib
{
    public class CsvFile
    {
        public string NameFile { get; }
        public string Dir { get; }
        private List<List<string>> _matriz;
        private int _lineNumber;
        private Encoding _encodingType;
        private StreamReader _reader;
        private const long MininumFileLengthValid = 100;
        private List<string> _line;
        private bool _bigFile;
        private const char Separator = ';';

        /// <summary>
        /// devolve um valor entre 0 e 1 que representa a precentagem lida do ficheiro
        /// </summary>
        public double Complet
        {
            get
            {
                if (_bigFile) return _matriz?.Count ?? 0;
                return ((double) BytesReaded) / FileSize;
            }
        }

        public long BytesReaded { get; private set; }
        public long FileSize { get; }

        /// <summary>
        /// construtor da class CsvFile
        /// </summary>
        /// <param name="nameFile">nome do ficheiro</param>
        /// <param name="dir">diretoria onde o ficheiro se encontra, usar só se o nome não incluir o caminho</param>
        /// <param name="open">indica se é para abrir a para com o Construtor, caso não invoque o metodo Open()</param>
        public CsvFile(string nameFile, string dir = "", bool open = false)
        {
            NameFile = nameFile;
            if (dir.Length > 0)
                Dir = dir + "\\";
            _matriz = new List<List<string>>();

            if (FileExists())
            {
                var fileInfo = new FileInfo(Dir + NameFile);
                FileSize = fileInfo.Length;
                if (open)
                    Open();
            }
        }

        /// <summary>
        /// Abre o ficheiro em causa
        /// </summary>
        /// <returns>false se correr mal, true se for aberto com sucesso</returns>
        public bool Open()
        {
            try
            {
                _reader = new StreamReader(Dir + NameFile);
                _encodingType = _reader.CurrentEncoding;
                var line = _reader.ReadLine();
                if (line != null)
                {
                    _line = new List<string>(line.Split(Separator));
                    BytesReaded += _encodingType.GetByteCount(line);
                }
                else
                {
                    _line = null;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"{DateTime.Now:G} {e}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Lê o ficheiro completo, colucando-o em memoria -> requer algum esforço de Ram
        /// </summary>
        /// <returns>numero de linhas do ficheiro</returns>
        [Obsolete]
        public int ReadFile()
        {
            _bigFile = true;
            StreamReader file;
            try
            {
                file = new StreamReader(Dir + NameFile);
            }
            catch (FileNotFoundException)
            {
                Console.Error.WriteLine($"{DateTime.Now:G}<{GetType().Name}> File not found: {Dir}{NameFile}");
                throw;
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine($"{DateTime.Now:G} <{GetType().Name}> {ex.Message}");
                throw;
            }

            string line;
            while ((line = file.ReadLine()) != null)
            {
                var col = line.Split(Separator);
                _matriz.Add(new List<string>(col));
            }

            file.Close();
            return _matriz.Count;
        }

        /// <summary>
        /// Lê um linha do ficheiro
        /// </summary>
        /// <returns>lista com cada celula da linha lida</returns>
        public List<string> GetNextLine()
        {
            if (_bigFile)
                return HasNextLine() ? _matriz[_lineNumber++] : null;
            var str = _line;
            var line = _reader.ReadLine();

            if (line != null)
            {
                _line = new List<string>(line.Split(Separator));
                BytesReaded += _encodingType.GetByteCount(line);
            }
            else
            {
                _line = null;
            }

            return str;
        }

        /// <summary>
        /// Verificar se exite um proxima linha
        /// </summary>
        /// <returns>true se existir, false se não</returns>
        public bool HasNextLine()
        {
            if (_bigFile)
                return _lineNumber < (_matriz.Count - 1);
            else
            {
                return _line != null;
            }
        }

        /// <summary>
        /// obtem a linha do indice i, apenas valida apenas quando usou ReadFile(), quando não, retorna a linha atual, mas não avança para a proxima
        /// </summary>
        /// <param name="i">Indice da linha, sem efeito sem o ReadLine() ser invocado</param>
        /// <returns>lista com cada celula da linha lida</returns>
        public List<string> GetLine(int i)
        {
            if (_bigFile && i >= 0 || i < _matriz.Count)
                return _matriz[i];
            else
            {
                return _line;
            }
        }

        /// <summary>
        /// Fecha o ficheiro, caso tenha invocado o metodo ReadFile(), elimina o conteudo em memoria
        /// </summary>
        public void Close()
        {
            _matriz = null;
            _reader.Close();
            GC.Collect();
        }

        /// <summary>
        /// Verifica que o ficheiro existe
        /// </summary>
        /// <returns>true se existir, false se não</returns>
        public bool FileExists()
        {
            return File.Exists(Dir + NameFile);
        }

        /// <summary>
        /// Verifica se o ficheiro tem mais do que o numero de bytes especificados
        /// </summary>
        /// <returns>true se tiver, false se não</returns>
        public bool IsValid()
        {
            return FileSize > MininumFileLengthValid;
        }
    }
}