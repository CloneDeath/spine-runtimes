using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;
using OpenTK;

namespace Spine
{
	public class Avatar {
		public Vector3 Position;
		public Vector2 Scale;

		private SkeletonRenderer SkeletonRenderer;
		private Skeleton Skeleton;
		private AnimationState State;
		private AnimationStateData StateData;

		public Avatar(string AnimationFile)
		{
			SkeletonRenderer = new SkeletonRenderer(Vector3.UnitY, Vector3.UnitZ);

			String name = AnimationFile;

			Atlas atlas = new Atlas(name + ".atlas", new OpenTKTextureLoader());
			SkeletonJson json = new SkeletonJson(atlas);
			Skeleton = new Skeleton(json.ReadSkeletonData(name + ".json"));
			Skeleton.SetSlotsToSetupPose();

			// Define mixing between animations.
			StateData = new AnimationStateData(Skeleton.Data);
			State = new AnimationState(StateData);

			Skeleton.X = 0;
			Skeleton.Y = 0;
			Skeleton.UpdateWorldTransform();
		}

		public void Update(double DeltaTime)
		{
			State.Update((float)DeltaTime);
		}


		public void Draw2D(Vector2 Position) {
			this.Draw2D(Position, Skeleton.RootBone.ScaleX, Skeleton.RootBone.ScaleY);
		}

		public void Draw2D(Vector2 Position, float Width, float Height) {
			State.Apply(Skeleton);
			Skeleton.X = Position.X;
			Skeleton.Y = Position.Y;
			
			//Copy Scale
			float scaleX = Skeleton.RootBone.ScaleX;
			float scaleY = Skeleton.RootBone.ScaleY;
			Skeleton.RootBone.ScaleX = Width;
			Skeleton.RootBone.ScaleY = Height;
			Skeleton.UpdateWorldTransform();
			SkeletonRenderer.Draw(Skeleton);

			//Restore Scale
			Skeleton.RootBone.ScaleX = scaleX;
			Skeleton.RootBone.ScaleY = scaleY;
		}

		public void Draw3D(float X, float Y, float Z)
		{
			State.Apply(Skeleton);
			Skeleton.RootBone.ScaleX = Scale;
			Skeleton.RootBone.ScaleY = Scale;
			Skeleton.UpdateWorldTransform();
			SkeletonRenderer.Draw(Skeleton, Z);
		}
	}
}
