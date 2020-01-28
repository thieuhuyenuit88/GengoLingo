using System;
using UnityEngine;

public class LessonData : ScriptableObject
{
    [Serializable]
    public class Lesson
    {
        public int id;
        public int topic_id;
        public string title;
        public int order;
        public string en, vi, zh, fr, de, hi, ind, it, ko, ms, pt, ru, es, th, tl, ar, pl, hr, nl, el, hu, sv, cs, ro, bg, da, tr, fi, sl, fa, bn, uk, am, km, si, zu, sw, lo, no;
    }

    public Lesson[] mListLessons;
}
