﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapTileGenerator.Core
{
    public class ArcServerLocalTileProvider : WmtsSourceProvider 
    {
        private int offsetZoom;

        public ArcServerLocalTileProvider(MapConfig config) : 
                base(config)
        {
            this.offsetZoom = config.OffsetZoom;
        }

        public override string GetRequestUrl(TileCoord tileCoord)
        {
            var x = 'C' + padLeft(Convert.ToInt32(tileCoord.X), 8, 16);
            var y = 'R' + padLeft(Convert.ToInt32(tileCoord.Y), 8, 16);//WMTS
            //var y = 'R' + padLeft(Convert.ToInt32(-1 * tileCoord.Y -1), 8, 16);//TMS
            var z = 'L' + padLeft(Convert.ToInt32(tileCoord.Zoom) + offsetZoom, 2, 10);

            string url = this._url;
            url = url.Replace("{z}", z);
            url = url.Replace("{y}", y);
            url = url.Replace("{x}", x);
            return url;
        }

        //将10进制转16进制，余位补0，凑成ArcServer的切片格式
        private string  padLeft(int val, int num, int radix =10)
        {
            var str = Convert.ToString(val, radix);
            return str.PadLeft(num, '0');
        }

    }
}
