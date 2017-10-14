# ElasticSharp
ElasticSharp is a cross platform native implementation of Elastic coin in C#.

Platforms

.Net Standard and .Net Core

Supported Features

1. Native implementation in C#. Not dependent on api.
2. Generates a new address from mnemonic on the fly.
3. Creates a new transaction and decodes existing transaction and signs a transaction with a secret phrase. Currently supports basic transaction only (i.e amount ones still working on message and attachment transaction types).
4. Cross platform support (works on mac, linux and windows). Built using .Net Standard. 

Pending Features

1. To support message, attachment transactions
2. Sending transaction bytes to server. This will prevent sending clear passwords to server.
3. Mobile apps
4. Wallet GUI in all platforms without complex installations.
5. Standalone client listening other nodes in network.
6. Node RPC/JSON api 
