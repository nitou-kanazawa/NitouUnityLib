using System.Collections.Generic;

// [参考]
//  qiita: ParticleSystem の再生位置を簡単に変えられる拡張メソッド https://qiita.com/Yamara/items/221868b0decc6364b56f

namespace UnityEngine {

    public static class ParticleSystemExtensions {


        /// ----------------------------------------------------------------------------


        //public static ParticleSystem Skip(this ParticleSystem self, float second, bool withChildren = true, bool fixedTimeStep = true) {
        //    if (second == 0f) return self;
        //    var isPlayed = self.isPlaying;

        //    if (second >= 0f) self.Simulate(second, withChildren, false, fixedTimeStep);
        //    else self.Simulate(self.totalTime + second, withChildren, true, fixedTimeStep);

        //    if (isPlayed) self.Play();
        //    return self;
        //}

        public static ParticleSystem SetTime(this ParticleSystem self, float time, bool withChildren = true, bool fixedTimeStep = true) {
            var isPlayed = self.isPlaying;
            self.Simulate(time, withChildren, true, fixedTimeStep);
            if (isPlayed) self.Play();
            return self;
        }
    }
}
