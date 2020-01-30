using System;
using UnityEngine;

public class TopicData : ScriptableObject
{
    [Serializable]
    public class Topic
    {
        public int id;
        public string title;
        public int order;
        public Sprite icon;
        public string en, vi, zh, fr, de, hi, ind, it, ko, ms, pt, ru, es, th, tl, ar, pl, hr, nl, el, hu, sv, cs, ro, bg, da, tr, fi, sl, fa, bn, uk, am, km, si, zu, no, sw, lo;
    }

    public Topic[] mListTopics;
}
