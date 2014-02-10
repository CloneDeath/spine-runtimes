using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spine;
using System.Diagnostics;

namespace PokemonSmash {
	public class Animation {
		public SkeletonRenderer skeletonRenderer;
		public Skeleton skeleton;
		public AnimationState state;
		public AnimationStateData stateData;
		public Stopwatch drawtime;

		public float Z = 0.0f;
		public float Scale = 1.0f / 50.0f;

		public Animation(string AnimationFile) {
			skeletonRenderer = new SkeletonRenderer();

			String name = AnimationFile;

			Atlas atlas = new Atlas("Data/" + name + ".atlas", new OpenTKTextureLoader());
			SkeletonJson json = new SkeletonJson(atlas);
			skeleton = new Skeleton(json.ReadSkeletonData("Data/" + name + ".json"));
			skeleton.SetSlotsToSetupPose();

			// Define mixing between animations.
			stateData = new AnimationStateData(skeleton.Data);
			state = new AnimationState(stateData);
			//state.SetAnimation("idle", true);

			skeleton.X = 0;
			skeleton.Y = 0.1f;
			skeleton.UpdateWorldTransform();

			drawtime = new Stopwatch();
			drawtime.Start();
		}


		void Draw() {
			float dt = drawtime.ElapsedMilliseconds / 1000.0f;
			drawtime.Restart();

			state.Update(dt);
			state.Apply(skeleton);
			skeleton.RootBone.ScaleX = Scale;
			skeleton.RootBone.ScaleY = Scale;
			skeleton.UpdateWorldTransform();
			skeletonRenderer.Draw(skeleton, Z);
		}
	}
}
