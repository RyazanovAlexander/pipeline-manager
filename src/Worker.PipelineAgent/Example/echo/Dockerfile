FROM aryazanov/command-executor:0.0.1 AS command-executor

FROM alpine:3.13.5
WORKDIR /
COPY --from=command-executor /bin/command-executor /bin/command-executor

USER 65535:65535
ENTRYPOINT ["command-executor"]