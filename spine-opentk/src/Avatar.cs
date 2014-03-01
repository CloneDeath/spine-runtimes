using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;

namespace Spine
{
	public class Avatar {
		public SkeletonRenderer skeletonRenderer;
		public Skeleton Skeleton;
		public AnimationState state;
		public AnimationStateData stateData;
		public float Scale = 1.0f;

		public Avatar(string AnimationFile)
		{
			skeletonRenderer = new SkeletonRenderer(true);

			String name = AnimationFile;

			Atlas atlas = new Atlas(name + ".atlas", new OpenTKTextureLoader());
			SkeletonJson json = new SkeletonJson(atlas);
			Skeleton = new Skeleton(json.ReadSkeletonData(name + ".json"));
			Skeleton.SetSlotsToSetupPose();

			// Define mixing between animations.
			stateData = new AnimationStateData(Skeleton.Data);
			state = new AnimationState(stateData);

			Skeleton.X = 0;
			Skeleton.Y = 0.1f;
			Skeleton.UpdateWorldTransform();
		}

		public void SetSkin(string SkinName) {
			Skeleton.SetSkin(SkinName);
			Skeleton.SetSlotsToSetupPose();
		}

		public void SetAnimation(string Name, bool Loop = false) {
			if(state.GetCurrent(0) != null && state.GetCurrent(0).Animation != null) {
				if(state.GetCurrent(0).Animation.Name != Name) {
					state.SetAnimation(0, Name, Loop);
				}
			} else {
				state.SetAnimation(0, Name, Loop);
			}
		}

		public void Update(float dt)
		{
			state.Update(dt);
			Skeleton.Update(dt);
		}

		public EventData FindEvent(string Name) {
			return Skeleton.Data.FindEvent(Name);
		}

		public BoundingBoxAttachment GetBoundingBox(string Name) {
			Slot slot = Skeleton.DrawOrder.Find(s => s.Attachment.Name == Name);
			if(slot != null && slot.Attachment is BoundingBoxAttachment) {
				BoundingBoxAttachment template = slot.Attachment as BoundingBoxAttachment;
				BoundingBoxAttachment ret = new BoundingBoxAttachment(template.Name);
				ret.Vertices = new float[template.Vertices.Count()];
				template.Vertices.CopyTo(ret.Vertices, 0);
				ret.ComputeWorldVertices(Skeleton.X, Skeleton.Y, slot.Bone, ret.Vertices);
				return ret;
			} else {
				return null;
			}
		}

		public void Draw2D(float X, float Y) {
			state.Apply(Skeleton);
			Skeleton.X = X;
			Skeleton.Y = Y;
			Skeleton.RootBone.ScaleX = Scale;
			Skeleton.RootBone.ScaleY = Scale;
			Skeleton.UpdateWorldTransform();
			skeletonRenderer.Draw(Skeleton);
		}

		public void Draw2D(float X, float Y, float width, float height) {
			state.Apply(Skeleton);
			Skeleton.X = X;
			Skeleton.Y = Y;
			float scaleX = Skeleton.RootBone.ScaleX;
			float scaleY = Skeleton.RootBone.ScaleY;
			Skeleton.RootBone.ScaleX = width;
			Skeleton.RootBone.ScaleY = height;
			Skeleton.UpdateWorldTransform();
			skeletonRenderer.Draw(Skeleton);
			Skeleton.RootBone.ScaleX = scaleX;
			Skeleton.RootBone.ScaleY = scaleY;
		}
	}
}
