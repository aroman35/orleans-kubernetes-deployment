dotnet publish `
-p PublishProfile=DefaultContainer `
-p ContainerImageTags=1.0.5 `
-p ContainerRepository=aroman35/orleans-kubernetes-silo

docker push aroman35/orleans-kubernetes-silo:1.0.5
