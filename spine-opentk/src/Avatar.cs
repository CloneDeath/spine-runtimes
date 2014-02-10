using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;

namespace Spine
{
	public class Avatar {
		public SkeletonRenderer skeletonRenderer;
		public Skeleton skeleton;
		public AnimationState state;
		public AnimationStateData stateData;
		public float Scale = 1.0f;

		public Avatar(string AnimationFile)
		{
			skeletonRenderer = new SkeletonRenderer(true);

			String name = AnimationFile;

			Atlas atlas = new Atlas(name + ".atlas", new OpenTKTextureLoader());
			SkeletonJson json = new SkeletonJson(atlas);
			skeleton = new Skeleton(json.ReadSkeletonData(name + ".json"));
			skeleton.SetSlotsToSetupPose();

			// Define mixing between animations.
			stateData = new AnimationStateData(skeleton.Data);
			state = new AnimationState(stateData);

			skeleton.X = 0;
			skeleton.Y = 0.1f;
			skeleton.UpdateWorldTransform();
		}

		public void Update(float dt)
		{
			state.Update(dt);
		}


		public void Draw2D(float X, float Y) {
			state.Apply(skeleton);
			skeleton.X = X;
			skeleton.Y = Y;
			skeleton.RootBone.ScaleX = Scale;
			skeleton.RootBone.ScaleY = Scale;
			skeleton.UpdateWorldTransform();
			skeletonRenderer.Draw(skeleton);
		}

		public void Draw2D(float X, float Y, float width, float height) {
			state.Apply(skeleton);
			skeleton.X = X;
			skeleton.Y = Y;
			float scaleX = skeleton.RootBone.ScaleX;
			float scaleY = skeleton.RootBone.ScaleY;
			skeleton.RootBone.ScaleX = width;
			skeleton.RootBone.ScaleY = height;
			skeleton.UpdateWorldTransform();
			skeletonRenderer.Draw(skeleton);
			skeleton.RootBone.ScaleX = scaleX;
			skeleton.RootBone.ScaleY = scaleY;
		}
	}
}
