﻿using AnyStatus.API;
using AnyStatus.Plugins.Elasticsearch.Helpers;
using System.Threading;
using System.Threading.Tasks;

namespace AnyStatus.Plugins.Elasticsearch.Node.Ram
{
    public class NodeRamUsageHandler : IRequestHandler<MetricQueryRequest<NodeRamUsageWidget>>
    {
        private readonly ElasticsearchHelper elasticsearchHelper;
        public NodeRamUsageHandler() : this(new ElasticsearchHelper()) { }

        public NodeRamUsageHandler(ElasticsearchHelper elasticsearchHelper)
        {
            this.elasticsearchHelper = elasticsearchHelper;
        }

        public async Task Handle(MetricQueryRequest<NodeRamUsageWidget> request, CancellationToken cancellationToken)
        {
            var clusterHealthWidget = request.DataContext;

            var client = elasticsearchHelper.GetElasticClient(clusterHealthWidget);

            var clusterStatsResponse = await client.StatsAsync("nodes.os.mem.used_percent", clusterHealthWidget.NodeId, cancellationToken);

            if (clusterStatsResponse.IsValid)
            {
                request.DataContext.Value = clusterStatsResponse.Nodes.OperatingSystem.Memory.UsedPercent;
                request.DataContext.State = State.Ok;
            }
            else
            {
                clusterHealthWidget.State = State.Invalid;
            }
        }
    }
}
