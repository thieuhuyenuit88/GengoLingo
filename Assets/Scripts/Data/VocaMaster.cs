using System;
using UnityEngine;

public class VocaMaster : ScriptableObject
{
    [Serializable]
    public class Voca
    {
        public int id;
        public int lesson_id;
        public Sprite icon;
        public string word;
        public string romaji;
        public string hiragana;
        public string sound_check;
        public string en, vi, zh, fr, de, hi, ind, it, ko, ms, pt, ru, es, th, tl, ar, pl, hr, nl, el, hu, sv, cs, ro, bg, da, tr, fi, sl, fa, bn, uk, am, km, si, zu, sw, lo, no;
    }

    public Voca[] mListVoca;
}
