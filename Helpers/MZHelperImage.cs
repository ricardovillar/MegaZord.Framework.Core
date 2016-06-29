using ImageProcessorCore;
using ImageProcessorCore.Formats;
using ImageProcessorCore.Samplers;
using System;
using System.IO;

namespace MegaZord.Framework.Helpers {
    public static class MZHelperImage {

        public static void SaveToFile(this ImageBase source, string fileName) {

            if (File.Exists(fileName))
                File.Delete(fileName);
            using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
                using (var memory = new MemoryStream()) {
                    ((Image)source).Save(memory);
                    memory.Position = 0;
                    var bytes = memory.ToArray();
                    file.Write(bytes, 0, bytes.Length);
                }
            }
        }
        /// <summary>
        /// Baseado em uma string base64 retorna uma imagem
        /// </summary>
        /// <param name="base64String">String base64 da imagem</param>
        /// <returns>Imagem representada pela string</returns>
        public static Image Base64ToImage(string base64String) {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);


            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            var image = new Image(ms);
            return image;
        }

        /// <summary>
        /// Baseado em uma imagem gera a string base64
        /// </summary>
        /// <param name="image">Imagem que deseja converter em base64</param>
        /// <param name="format">Formato da imagem</param>
        /// <returns>string base64 da imagem</returns>
        public static string ImageToBase64(Image image, IImageFormat format) {
            using (MemoryStream ms = new MemoryStream()) {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }


        /// <summary>
        /// Retornar um array de bytes da imagem na nova dimensão
        /// </summary>
        /// <param name="newWidth">Nova largura</param>
        /// <param name="newHeight">Nova altura</param>
        /// <param name="originalFIle">array de bytes original da imagem</param>
        /// <returns>array de bytes da nova imagem</returns>
        public static byte[] ResizeImage(int newWidth, int newHeight, byte[] originalFIle) {

            //using (FileStream stream = File.OpenRead("foo.jpg"))
            //using (FileStream output = File.OpenWrite("bar.jpg"))
            using (var strOut = new MemoryStream()) {
                using (var strIn = new MemoryStream(originalFIle)) {
                    using (Image image = new Image(strIn)) {
                        image.Resize(image.Width / 2, image.Height / 2).Save(strOut);
                        return strOut.ToArray();
                    }
                }
            }
        }

        /// <summary>
        /// Retornar um array de bytes da imagem na nova dimensão
        /// </summary>
        /// <param name="newWidth">Nova largura</param>
        /// <param name="newHeight">Nova altura</param>
        /// <param name="imagePath">Caminho da imagem</param>
        /// <returns>array de bytes da nova imagem</returns>
        /// 
        public static byte[] ResizeImage(int newWidth, int newHeight, string imagePath) {
            return MZHelperImage.ResizeImage(newWidth, newHeight, File.ReadAllBytes(imagePath));

        }

        /// <summary>
        /// Retornar um array de bytes da imagem na nova dimensão
        /// </summary>
        /// <param name="newWidth">Nova largura</param>
        /// <param name="newHeight">Nova altura</param>
        /// <param name="image">Imagem</param>
        /// <param name="format">formato da imagem</param>
        /// <returns></returns>
        public static byte[] ResizeImage(int newWidth, int newHeight, Image image, IImageFormat format) {
            byte[] bytes = null;
            using (var ms = new MemoryStream()) {
                image.Save(ms, format);
                bytes = ms.ToArray();
            }
            return ResizeImage(newWidth, newHeight, bytes);
        }


        /// <summary>
        /// Cria uma imagem baseado em um array de bytes
        /// </summary>
        /// <param name="bytes">array de bytes da image</param>
        /// <returns>Imagem criada</returns>
        public static Image CreateImagem(byte[] bytes) {
            Image img = null;
            using (var ms = new MemoryStream(bytes)) {
                img = new Image(ms);

            }
            return img;
        }


         
    }
}
