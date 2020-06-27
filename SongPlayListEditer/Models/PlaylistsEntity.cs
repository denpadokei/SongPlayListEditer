using BeatSaberMarkupLanguage.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SongPlayListEditer.Models
{
    public class PlaylistsEntity
    {
        [UIObject("cover")]
        public Sprite Cover { get; set; }

        [UIValue("name")]
        public string Name { get; set; }

        [UIAction("add-click")]
        public void AddHandle()
        {

        }
    }
}
