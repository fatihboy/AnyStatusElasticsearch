﻿using AnyStatus.API;
using System.Threading;
using System.Threading.Tasks;
using AnyStatus.Plugins.Elasticsearch.Helpers;

namespace AnyStatus.Plugins.Elasticsearch.Node.StoreSize
{
    public class StoreSizeMetricQuery : IMetricQuery<StoreSizeWidget>
    {
        public async Task Handle(MetricQueryRequest<StoreSizeWidget> request, CancellationToken cancellationToken)
        {
            var storeSizeWidget = request.DataContext;

            var client = ElasticsearchHelper.GetElasticClient(storeSizeWidget);

            var clusterStatsResponse = await client.StatsAsync("indices.store.size_in_bytes", storeSizeWidget.NodeId, cancellationToken);

            if (clusterStatsResponse.IsValid)
            {
                request.DataContext.Value = FileSizeFormatter.FormatSize(clusterStatsResponse.Indices.Store.SizeInBytes);
                request.DataContext.State = State.Ok;
            }
            else
            {
                storeSizeWidget.State = State.Invalid;
            }
        }
    }
}
