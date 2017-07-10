using System;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using System.IO;

namespace BLL
{
    public class ProcessFile
    {
        /// <summary>
        ///  Processamento de dados dos arquivos PDF.
        /// </summary>
        /// <param name="_inFile">Lista de arquivos do diretório</param>
        /// <param name="_title">Titulo a ser atribuído ao metadado dos arquivos</param>
        public static string Processamento(ViewModel.Arquivo.Arquivos _arqs)
        {
            
            try
            {
                Directory.CreateDirectory(_arqs.diretorio + @"\sanitized\");
                string outFile = _arqs.diretorio + @"\sanitized\";

                foreach (var item in _arqs.files)
                {
                    PdfReader _reader = new PdfReader(item.FullName);
                    using (FileStream fs = new FileStream(outFile + item.Name, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        using (PdfStamper stamper = new PdfStamper(_reader, fs))
                        {
                            Dictionary<String, String> info = _reader.Info;

                            info.Remove("Title");
                            //info.Remove("Author");
                            //info.Remove("Subject");

                            info.Add("Title", _arqs.title);
                            //info.Add("Author", _arqs.author);
                            //info.Add("Subject", _arqs.subject);

                            stamper.MoreInfo = info;
                            stamper.Close();
                        }
                    }
                }
                return "Arquivos processados";
            }
            catch (Exception ex)
            {
                Log.LogarError(ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}
