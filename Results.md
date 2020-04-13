## Simplest port
|                  Method |      Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |----------:|----------:|----------:|------:|------:|------:|----------:|
| BlockingCollectionQueue | 25.240 ms | 0.4996 ms | 0.4673 ms |     - |     - |     - |      64 B |
|  NoDedicatedThreadQueue |  6.829 ms | 0.1100 ms | 0.0975 ms |     - |     - |     - |     210 B |
|                 RxQueue | 13.293 ms | 0.2315 ms | 0.2165 ms |     - |     - |     - |      64 B |
|           ChannelsQueue |  5.232 ms | 0.0379 ms | 0.0355 ms |     - |     - |     - |     596 B |
|        TPLDataflowQueue |  5.637 ms | 0.0691 ms | 0.0646 ms |     - |     - |     - |   15104 B |
|          DisruptorQueue |  1.848 ms | 0.0364 ms | 0.0709 ms |     - |     - |     - |      64 B |

## Split setup/cleanup
|                  Method |      Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |----------:|----------:|----------:|------:|------:|------:|----------:|
| BlockingCollectionQueue | 26.211 ms | 0.5096 ms | 0.4767 ms |     - |     - |     - |      64 B |
|  NoDedicatedThreadQueue |  6.523 ms | 0.0902 ms | 0.0799 ms |     - |     - |     - |     179 B |
|                 RxQueue | 12.662 ms | 0.2509 ms | 0.2464 ms |     - |     - |     - |     132 B |
|           ChannelsQueue |  4.838 ms | 0.0952 ms | 0.1304 ms |     - |     - |     - |     783 B |
|        TPLDataflowQueue |  5.715 ms | 0.0790 ms | 0.0739 ms |     - |     - |     - |   18254 B |
|          DisruptorQueue |  1.942 ms | 0.0758 ms | 0.2024 ms |     - |     - |     - |      64 B |

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
