﻿/*
Anystatus Elasticsearch plugin
Copyright 2019 Fatih Boy

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */
using AnyStatus.API;
using AnyStatus.Plugins.Elasticsearch.Shared;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace AnyStatus.Plugins.Elasticsearch.RamUsage
{
    [DisplayName("RAM Usage")]
    [DisplayColumn("Elasticsearch")]
    [Description("Shows the percentage of RAM usage for the Elasticsearch Node/Cluster")]
    public class RamUsageWidget : Sparkline, IElasticsearchWidget, ISchedulable
    {
        [Url]
        [Required]
        [PropertyOrder(10)]
        [Category("RAM Usage")]
        [DisplayName("Node Uris")]
        [Description("Elasticsearch node uris to connect")]
        public List<string> NodeUris { get; set; }

        [Category("RAM Usage")]
        [PropertyOrder(20)]
        [DisplayName("Node Id")]
        [Description("Elasticsearch node id. Leave empty to watch Cluster RAM usage")]
        public string NodeId { get; set; }

        [Category("RAM Usage")]
        [PropertyOrder(30)]
        [RefreshProperties(RefreshProperties.All)]
        [DisplayName("Use Basic Authentication")]
        [Description("Use Basic Authentication to connect Elasticsearch Cluster")]
        public bool UseBasicAuthentication
        {
            get => useBasicAuthentication;
            set
            {
                useBasicAuthentication = value;

                OnPropertyChanged();

                SetPropertyVisibility(nameof(Username), useBasicAuthentication);
                SetPropertyVisibility(nameof(Password), useBasicAuthentication);
            }
        }

        [Browsable(true)]
        [Category("RAM Usage")]
        [PropertyOrder(40)]
        [Description("Username to connect Elasticsearch Cluster")]
        public string Username { get; set; }

        [Browsable(true)]
        [Category("RAM Usage")]
        [PropertyOrder(50)]
        [Description("Password to connect Elasticsearch Cluster")]
        [Editor(typeof(PasswordEditor), typeof(PasswordEditor))]
        public string Password { get; set; }

        [Category("RAM Usage")]
        [PropertyOrder(60)]
        [DisplayName("Trust Certificate")]
        [Description("Always trust server certificate")]
        public bool TrustCertificate { get; set; }

        private bool useBasicAuthentication;

        public RamUsageWidget()
        {
            Name = "RAM Usage";
            Symbol = "%";
            MaxValue = 100;
            Interval = 1;
            Units = IntervalUnits.Minutes;
            UseBasicAuthentication = false;
        }
    }
}
