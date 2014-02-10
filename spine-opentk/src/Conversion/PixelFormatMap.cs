using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imaging = System.Drawing.Imaging;
using OpenGL = OpenTK.Graphics.OpenGL;

namespace Spine.Conversion
{
	internal class PixelFormatMap
	{
		private static readonly Dictionary<Format, PixelFormatMap> FormatTranslator = new Dictionary<Format, PixelFormatMap> {
			{ Format.Alpha,				new PixelFormatMap(Imaging.PixelFormat.DontCare, OpenGL.PixelFormat.Alpha, OpenGL.PixelInternalFormat.One) },
			{ Format.Intensity,			null },
			{ Format.LuminanceAlpha,	null },
			{ Format.RGB565,			new PixelFormatMap(Imaging.PixelFormat.Format16bppRgb565, OpenGL.PixelFormat.Rgb, OpenGL.PixelInternalFormat.R5G6B5IccSgix) },
			{ Format.RGB888,			new PixelFormatMap(Imaging.PixelFormat.Format24bppRgb, OpenGL.PixelFormat.Rgb, OpenGL.PixelInternalFormat.Rgb8) },
			{ Format.RGBA4444,			null },
			{ Format.RGBA8888,			new PixelFormatMap(Imaging.PixelFormat.Format32bppArgb, OpenGL.PixelFormat.Bgra, OpenGL.PixelInternalFormat.Four) },
		};

		public Imaging.PixelFormat PixelFormat;
		public OpenGL.PixelFormat OpenGLPixelFormat;
		public OpenGL.PixelInternalFormat InternalFormat;

		public PixelFormatMap(Imaging.PixelFormat Format, OpenGL.PixelFormat GLFormat, OpenGL.PixelInternalFormat numbytes)
		{
			this.PixelFormat = Format;
			this.OpenGLPixelFormat = GLFormat;
			this.InternalFormat = numbytes;
		}

		public static Imaging.PixelFormat GetPixelFormat(Format format)
		{
			if (!FormatTranslator.ContainsKey(format) || FormatTranslator[format] == null) {
				throw new NotImplementedException("The PixelFormat '" + format + "' is not currently supported.");
			}
			return FormatTranslator[format].PixelFormat;
		}

		public static OpenGL.PixelFormat GetOpenGLPixelFormat(Format format)
		{
			if (!FormatTranslator.ContainsKey(format) || FormatTranslator[format] == null) {
				throw new NotImplementedException("The OpenGLPixelFormat '" + format + "' is not currently supported.");
			}
			return FormatTranslator[format].OpenGLPixelFormat;
		}

		internal static OpenGL.PixelInternalFormat GetInternalFormat(Format format)
		{
			if (!FormatTranslator.ContainsKey(format) || FormatTranslator[format] == null) {
				throw new NotImplementedException("The InternalFormat '" + format + "' is not currently supported.");
			}
			return FormatTranslator[format].InternalFormat;
		}
	}
}
