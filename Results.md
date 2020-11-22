## Simplest port (results not reliable)
|                  Method |      Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |----------:|----------:|----------:|------:|------:|------:|----------:|
| BlockingCollectionQueue | 25.078 ms | 0.5016 ms | 0.8784 ms |     - |     - |     - |      64 B |
|  NoDedicatedThreadQueue |  7.070 ms | 0.1406 ms | 0.2966 ms |     - |     - |     - |     290 B |
|                 RxQueue | 12.578 ms | 0.2476 ms | 0.4272 ms |     - |     - |     - |      64 B |
|           ChannelsQueue |  5.117 ms | 0.0855 ms | 0.0714 ms |     - |     - |     - |     248 B |
|        TPLDataflowQueue |  5.914 ms | 0.1083 ms | 0.1519 ms |     - |     - |     - |   17021 B |
|          DisruptorQueue |  4.501 ms | 0.1862 ms | 0.5461 ms |     - |     - |     - |      64 B |

## Split setup/cleanup
|                  Method |      Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |----------:|----------:|----------:|------:|------:|------:|----------:|
| BlockingCollectionQueue | 24.608 ms | 0.4528 ms | 0.4236 ms |     - |     - |     - |      82 B |
|  NoDedicatedThreadQueue |  6.799 ms | 0.0760 ms | 0.0674 ms |     - |     - |     - |     250 B |
|                 RxQueue | 12.708 ms | 0.1390 ms | 0.1160 ms |     - |     - |     - |      73 B |
|           ChannelsQueue |  6.365 ms | 0.0672 ms | 0.0595 ms |     - |     - |     - |    1059 B |
|        TPLDataflowQueue |  5.855 ms | 0.0758 ms | 0.0709 ms |     - |     - |     - |   19858 B |
|          DisruptorQueue |  3.923 ms | 0.0563 ms | 0.0910 ms |     - |     - |     - |      64 B |

## MPSC on disruptor
|                  Method |      Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |----------:|----------:|----------:|------:|------:|------:|----------:|
|          DisruptorQueue |  6.022 ms | 0.1192 ms | 0.1784 ms |     - |     - |     - |      64 B |

## SPSC blocking disruptor
|         Method |     Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|---------:|---------:|------:|------:|------:|----------:|
| DisruptorQueue | 11.93 ms | 0.234 ms | 0.219 ms |     - |     - |     - |      73 B |

## MPSC blocking disruptor
|         Method |     Mean |    Error |   StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |---------:|---------:|---------:|------:|------:|------:|----------:|
| DisruptorQueue | 14.70 ms | 0.177 ms | 0.166 ms |     - |     - |     - |      64 B |

## Job size = 1M, Disruptor batch size = 10 
|                           Method |       Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------------- |-----------:|----------:|----------:|------:|------:|------:|----------:|
|          BlockingCollectionQueue | 243.979 ms | 4.6870 ms | 5.2095 ms |     - |     - |     - |      64 B |
|           NoDedicatedThreadQueue |  72.050 ms | 1.4265 ms | 3.0090 ms |     - |     - |     - |     283 B |
|                          RxQueue | 124.467 ms | 1.9153 ms | 1.7916 ms |     - |     - |     - |      64 B |
|                    ChannelsQueue |  53.814 ms | 1.0610 ms | 1.7134 ms |     - |     - |     - |    6685 B |
|                 TPLDataflowQueue |  55.875 ms | 0.7748 ms | 0.6868 ms |     - |     - |     - |  146428 B |
|                   DisruptorQueue |  45.520 ms | 0.4722 ms | 0.3943 ms |     - |     - |     - |      64 B |
|         DisruptorQueueNoDelegate |  43.143 ms | 0.8513 ms | 2.0882 ms |     - |     - |     - |         - |
|  DisruptorQueueNoDelegateBatched |   8.574 ms | 0.1226 ms | 0.1024 ms |     - |     - |     - |         - |

## Job size = 1M, Disruptor batch size = 10, No method calls in test
|                          Method |       Mean |     Error |     StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------------- |-----------:|----------:|-----------:|------:|------:|------:|----------:|
|         BlockingCollectionQueue | 261.535 ms | 5.9974 ms | 17.6836 ms |     - |     - |     - |      64 B |
|          NoDedicatedThreadQueue |  70.767 ms | 1.1073 ms |  0.9246 ms |     - |     - |     - |     269 B |
|                         RxQueue | 132.344 ms | 2.4868 ms |  2.3262 ms |     - |     - |     - |     380 B |
|                   ChannelsQueue |  53.065 ms | 1.0590 ms |  1.7399 ms |     - |     - |     - |    3590 B |
|                TPLDataflowQueue |  57.314 ms | 1.1087 ms |  1.6932 ms |     - |     - |     - |  214032 B |
|                  DisruptorQueue |  33.467 ms | 1.9658 ms |  5.7963 ms |     - |     - |     - |      64 B |
|        DisruptorQueueNoDelegate |  30.194 ms | 1.3844 ms |  4.0819 ms |     - |     - |     - |         - |
| DisruptorQueueNoDelegateBatched |   7.261 ms | 0.1412 ms |  0.2070 ms |     - |     - |     - |         - |
