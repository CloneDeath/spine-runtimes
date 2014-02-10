using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics.OpenGL;

namespace Spine.Conversion
{
	internal class TextureFilterMap
	{
		private static readonly Dictionary<TextureFilter, TextureFilterMap> FilterMap = new Dictionary<TextureFilter, TextureFilterMap> {
			{ TextureFilter.Linear,					new TextureFilterMap(TextureMinFilter.Linear,				TextureMagFilter.Linear) },
			{ TextureFilter.MipMap,					new TextureFilterMap(null,								    null)},
			{ TextureFilter.MipMapLinearLinear,		new TextureFilterMap(TextureMinFilter.LinearMipmapLinear,	null) },
			{ TextureFilter.MipMapLinearNearest,	new TextureFilterMap(TextureMinFilter.LinearMipmapNearest,	null) },
			{ TextureFilter.MipMapNearestLinear,	new TextureFilterMap(TextureMinFilter.NearestMipmapLinear,	null) },
			{ TextureFilter.MipMapNearestNearest,	new TextureFilterMap(TextureMinFilter.NearestMipmapNearest, null) },
			{ TextureFilter.Nearest,				new TextureFilterMap(TextureMinFilter.Nearest,				TextureMagFilter.Nearest) },
		};

		TextureMinFilter? MinFilter;
		TextureMagFilter? MagFilter;

		public TextureFilterMap(TextureMinFilter? min, TextureMagFilter? mag)
		{
			this.MinFilter = min;
			this.MagFilter = mag;
		}

		public static TextureMinFilter GetMinFilter(TextureFilter filter)
		{
			if (!FilterMap.ContainsKey(filter) || FilterMap[filter] == null || FilterMap[filter].MinFilter == null) {
				throw new NotImplementedException("The MinFilter '" + filter + "' is not currently supported.");
			}
			return (TextureMinFilter)FilterMap[filter].MinFilter;
		}

		public static TextureMagFilter GetMagFilter(TextureFilter filter)
		{
			if (!FilterMap.ContainsKey(filter) || FilterMap[filter] == null || FilterMap[filter].MagFilter == null) {
				throw new NotImplementedException("The MagFilter '" + filter + "' is not currently supported.");
			}
			return (TextureMagFilter)FilterMap[filter].MagFilter;
		}
	}
}
