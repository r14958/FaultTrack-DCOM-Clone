fdsc.pfx
FaultTrack Developer Server Certificate

This is a Server Authentication certificate for the FaultTrack server so it can be run locally for development and testing. The FaultTrack server's application configuration file should be configured so the WCF service will look for the certificate by its thumbprint via a service behavior that is shared with the various service endpoints.

Thumbprint:
89 3e 5c ab d6 a7 f1 15 22 44 0f 69 cc 53 68 0f 0c 31 a4 ee

It is recommended to import the certificate into the Current User (Personal) certificate store.