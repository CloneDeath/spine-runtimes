using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using Spine.Conversion;

namespace Spine {
	public class OpenTKTextureLoader : TextureLoader {
		public void Load (AtlasPage page, String path) {
			Bitmap texture = new Bitmap(path);

			BitmapData texturedata = texture.LockBits(new Rectangle(0, 0, texture.Width, texture.Height),
				ImageLockMode.ReadOnly, PixelFormatMap.GetPixelFormat(page.format));


			int tex = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, tex);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureFilterMap.GetMagFilter(page.magFilter));
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureFilterMap.GetMinFilter(page.minFilter));

			

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelFormatMap.GetInternalFormat(page.format), texture.Width, texture.Height, 0,
				PixelFormatMap.GetOpenGLPixelFormat(page.format), PixelType.UnsignedByte, texturedata.Scan0);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

			texture.UnlockBits(texturedata);

			
			page.rendererObject = tex;
			page.width = texture.Width;
			page.height = texture.Height;
		}

		public void Unload (Object texture) {
			GL.DeleteTexture((int)texture);
		}
	}
}
