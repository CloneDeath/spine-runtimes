using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spine {
	public class SpriteBatchItem {
		public int Texture;
		public VertexPositionColorTexture vertexTL = new VertexPositionColorTexture();
		public VertexPositionColorTexture vertexTR = new VertexPositionColorTexture();
		public VertexPositionColorTexture vertexBL = new VertexPositionColorTexture();
		public VertexPositionColorTexture vertexBR = new VertexPositionColorTexture();
	}
}
