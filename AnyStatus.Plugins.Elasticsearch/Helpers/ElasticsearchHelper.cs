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
using AnyStatus.Plugins.Elasticsearch.ElasticsearchClient;
using AnyStatus.Plugins.Elasticsearch.Shared;

namespace AnyStatus.Plugins.Elasticsearch.Helpers
{
    public class ElasticsearchHelper
    {

        public virtual ElasticsearchSimpleClient GetElasticClient(IElasticsearchWidget elasticsearchWidget)
        {
            var client = new ElasticsearchSimpleClient(elasticsearchWidget);
            return client;
        }
    }
}
