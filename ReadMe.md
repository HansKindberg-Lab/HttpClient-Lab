# HttpClient-Lab

Web-application with the function to do HttpClient-calls, System.Net.Http.HttpClient. That way we can investigate HttpClient-calls when running in a container.

![.github/workflows/Docker-deploy.yml](https://github.com/HansKindberg-Lab/HttpClient-Lab/actions/workflows/Docker-deploy.yml/badge.svg)

Web-application, without configuration, pushed to Docker Hub: https://hub.docker.com/r/hanskindberg/httpclient-lab

## Configuration example for forwarded headers

	{
		"ForwardedHeaders": {
			"ForwardedHeaders": "All"
		}
	}