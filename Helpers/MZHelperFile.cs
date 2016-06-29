using System.IO;
using System;
using System.Text;

namespace MegaZord.Framework.Helpers {
    public class MZHelperFile {
        /// <summary>
        /// Remove o arquivo informado como parâmetro
        /// </summary>
        /// <param name="fileName">Caminho completo do arquivo para ser exlcuido</param>
        public static void Delete(string fileName) {
            if (File.Exists(fileName))
                File.Delete(fileName);
        }
        /// <summary>
        /// Define se existe ou não um determinado arquivo
        /// </summary>
        /// <param name="fileName">Caminho completo do arquivo</param>
        /// <returns>True se o arquivo existir e false se não existir</returns>
        public static bool Exists(string fileName) {
            return File.Exists(fileName);
        }

        /// <summary>
        /// Cria um arquivo no caminho completo informado com o conteudo informado
        /// </summary>
        /// <param name="filename">Caminho do arquivo a ser criado</param>
        /// <param name="content">Conteúdo do arquivos</param>
        /// <returns>True se consegue criar o arquivo e false caso não consiga</returns>
        public static bool Create(string filename, string content) {
            var bcreated = true;

            if (!MZHelperFile.Exists(filename)) {
                var fi = new System.IO.FileInfo(filename);
                try {
                    //Create the file.
                    using (FileStream fs = fi.Create()) {
                        Byte[] info =
                            new UTF8Encoding(true).GetBytes(content);

                        //Add some information to the file.
                        fs.Write(info, 0, info.Length);
                        fs.Flush();

                    }
                }
                catch {
                    bcreated = false;

                }
            }
            return bcreated;
        }



    }
}
