using SongPlayListEditer.Statics;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SongPlayListEditer.Models
{
    internal class Base64Sprites
    {
        public static Sprite StarFullIcon;
        public static Sprite SpeedIcon;
        public static Sprite GraphIcon;
        public static Sprite DeleteIcon;
        public static Sprite XIcon;
        public static Sprite RandomIcon;
        public static Sprite DoubleArrow;
        public static ConcurrentDictionary<string, Texture2D> CashedTextuer { get; } = new ConcurrentDictionary<string, Texture2D>();
        private static string Base64StringHeader => "data:image/%extention%;base64";

        public static Stream Base64ToStream(string base64)
        {
            var base64string = base64.Split(',').Last();
            var body = Convert.FromBase64String(base64string);

            return new MemoryStream(body);
        }

        public static string SpriteToBase64(Sprite input) => Convert.ToBase64String(input.texture.EncodeToPNG());

        public static Sprite Base64ToSprite(string base64)
        {
            // prune base64 encoded image header
            var r = new Regex(@"data:image.*base64,");
            base64 = r.Replace(base64, "");

            Sprite s = null;
            try {
                var tex = Base64ToTexture2D(base64);
                s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), (Vector2.one / 2f));
            }
            catch (Exception) {
                Console.WriteLine("Exception loading texture from base64 data.");
                s = null;
            }

            return s;
        }

        public static Texture2D Base64ToTexture2D(string encodedData)
        {
            try {
                var imageData = Convert.FromBase64String(string.IsNullOrEmpty(encodedData) ? DefaultImage.DEFAULT_IMAGE : encodedData);

                int width, height;
                GetImageSize(imageData, out width, out height);

                Logger.Info($"W : {width}, H : {height}");

                var texture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
                texture.hideFlags = HideFlags.HideAndDontSave;
                texture.filterMode = FilterMode.Trilinear;
                texture.LoadImage(imageData);
                return texture;
            }
            catch (Exception e) {
                Logger.Error(e);
                var imageData = Convert.FromBase64String(DefaultImage.DEFAULT_IMAGE);

                int width, height;
                GetImageSize(imageData, out width, out height);

                Logger.Info($"W : {width}, H : {height}");

                var texture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
                texture.hideFlags = HideFlags.HideAndDontSave;
                texture.filterMode = FilterMode.Trilinear;
                texture.LoadImage(imageData);
                return texture;
            }
        }

        public static Texture2D ImageFileToTextuer2D(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                var body = new byte[stream.Length];
                var readByte = stream.Read(body, 0, (int)stream.Length);
                return Base64ToTexture2D(Convert.ToBase64String(body));
            }
        }

        public static Texture2D StreamToTextuer2D(Stream stream)
        {
            try {
                var imageData = new byte[stream.Length];

                var streamindex = stream.Read(imageData, 0, (int)stream.Length);

                int width, height;
                GetImageSize(imageData, out width, out height);

                Logger.Info($"W : {width}, H : {height}");

                var texture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
                texture.hideFlags = HideFlags.HideAndDontSave;
                texture.filterMode = FilterMode.Trilinear;
                texture.LoadImage(imageData);
                return texture;
            }
            catch (Exception e) {
                Logger.Error(e);
                var imageData = Convert.FromBase64String(DefaultImage.DEFAULT_IMAGE);

                int width, height;
                GetImageSize(imageData, out width, out height);

                Logger.Info($"W : {width}, H : {height}");

                var texture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);
                texture.hideFlags = HideFlags.HideAndDontSave;
                texture.filterMode = FilterMode.Trilinear;
                texture.LoadImage(imageData);
                return texture;
            }
        }

        public static Sprite StreamToSprite(Stream stream)
        {
            Sprite s = null;
            try {
                var tex = StreamToTextuer2D(stream);
                s = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), (Vector2.one / 2f));
            }
            catch (Exception) {
                Console.WriteLine("Exception loading texture from base64 data.");
                s = null;
            }

            return s;
        }

        public static Sprite ImageFileToSprite(string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                var body = new byte[stream.Length];
                var readByte = stream.Read(body, 0, (int)stream.Length);
                return Base64ToSprite(Convert.ToBase64String(body));
            }
        }

        private static void GetImageSize(byte[] imageData, out int width, out int height)
        {
            width = ReadInt(imageData, 3 + 15);
            height = ReadInt(imageData, 3 + 15 + 2 + 2);
        }

        private static int ReadInt(byte[] imageData, int offset) => (imageData[offset] << 8) | imageData[offset + 1];

        public static Texture2D LoadTextureRaw(byte[] file)
        {
            if (file.Count() > 0) {
                var Tex2D = new Texture2D(2, 2);
                if (Tex2D.LoadImage(file))
                    return Tex2D;
            }
            return null;
        }

        public static Texture2D LoadTextureFromResources(string resourcePath) => LoadTextureRaw(GetResource(Assembly.GetCallingAssembly(), resourcePath));

        public static Sprite LoadSpriteRaw(byte[] image, float PixelsPerUnit = 100.0f) => LoadSpriteFromTexture(LoadTextureRaw(image), PixelsPerUnit);

        public static Sprite LoadSpriteFromTexture(Texture2D SpriteTexture, float PixelsPerUnit = 100.0f)
        {
            if (SpriteTexture)
                return Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
            return null;
        }

        public static Sprite LoadSpriteFromResources(string resourcePath, float PixelsPerUnit = 100.0f) => LoadSpriteRaw(GetResource(Assembly.GetCallingAssembly(), resourcePath), PixelsPerUnit);

        public static string FileInfoToBase64(FileInfo fileinfo)
        {
            try {
                using (var stream = new FileStream(fileinfo.FullName, FileMode.Open, FileAccess.Read)) {
                    return StreamToBase64(stream, fileinfo.Extension);
                }
            }
            catch (Exception e) {
                Logger.Error(e);
                return GetBase64String("jpg", DefaultImage.DEFAULT_IMAGE);
            }
        }

        public static string StreamToBase64(Stream stream, string extention)
        {
            try {
                var body = new byte[stream.Length];
                var readByte = stream.Read(body, 0, (int)stream.Length);
                return GetBase64String(extention, Convert.ToBase64String(body));
            }
            catch (Exception e) {
                Logger.Error(e);
                return GetBase64String("jpg", DefaultImage.DEFAULT_IMAGE);
            }
        }

        public static string GetBase64String(string extention, string base64string) => $"{Base64StringHeader.Replace("%extention%", extention)},{base64string}";

        public static byte[] GetResource(Assembly asm, string ResourceName)
        {
            var stream = asm.GetManifestResourceStream(ResourceName);
            var data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            return data;
        }
    }
}
