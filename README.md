# snap7-to-prometheus
Getting PLC tags out of S7-1200/1500 and presenting them as metrics that can be read by prometheus

Metrics are available on http://localhost:5489/metrics

Config file is reloaded automatically but this doesn't work if config file is supplies via a docker volume.