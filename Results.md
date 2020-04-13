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
