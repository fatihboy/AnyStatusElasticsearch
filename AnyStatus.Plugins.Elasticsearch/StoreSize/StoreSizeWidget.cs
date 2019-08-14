﻿/*
Anystatus Elasticsearch plugin
Copyright (C) 2019  Enterprisecoding (Fatih Boy)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using AnyStatus.API;
using AnyStatus.Plugins.Elasticsearch.Shared;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnyStatus.Plugins.Elasticsearch.StoreSize
{
    [DisplayName("Store Size")]
    [DisplayColumn("Elasticsearch")]
    [Description("Shows store size taken by primary & replica shards for the Elasticsearch Node/Cluster")]
    public class StoreSizeWidget : Metric, IElasticsearchWidget, ISchedulable
    {

        [Required]
        [Category("Store Size")]
        [Description("Elasticsearch node uris to connect")]
        public List<string> NodeUris { get; set; }

        [Category("Store Size")]
        [Description("Use Basic Authentication to connect Elasticsearch Cluster")]
        public bool UseBasicAuthentication { get; set; }

        [Category("Store Size")]
        [Description("Username to connect Elasticsearch Cluster")]
        public string Username { get; set; }

        [Category("Store Size")]
        [Description("Password to connect Elasticsearch Cluster")]
        public string Password { get; set; }

        [Category("Store Size")]
        [Description("Always trust server certificate")]
        public bool TrustCertificate { get; set; }

        [Category("Store Size")]
        [Description("Elasticsearch node id. Leave empty to watch Cluster Store Size")]
        public string NodeId { get; set; }

        public StoreSizeWidget()
        {
            Name = "Store Size";

            Interval = 1;
            Units = IntervalUnits.Minutes;
        }
    }
}