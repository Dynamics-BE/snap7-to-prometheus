# snap7-to-prometheus
Getting PLC tags out of Siemens PLCs and presenting them as a plaintext metrics file that can be read by prometheus.

For Siemens S7-1200/1500, see the [notes](http://snap7.sourceforge.net/snap7_client.html#1200_1500)

## Build and run
* Either use docker to build and run the application (best for deployment on a server)
* Use Visual Studio debugging (easiest for testing)
* Build an exe using Visual Studio

## Using docker
* copy `example.docker-compose.yml` to `docker-compose.yml` and adjust for your purposes
* copy `./snap7-to-prometheus/config.yml` to `./config.yml` fill in your tags
* execute `sudo docker-compose up --build`

Configure prometheus to use the metrics file that is available on `http://<host>:5489/metrics`

## Config file
The config file `./snap7-to-prometheus/config.yml` has the connection details of the PLC and a list of tags that will be exposed in the metrics file.
This config file is automatically reloaded when changes are detected. Automatic config file reload does not work with docker, restart the container after changing config.

### Labels
Tags can have labels. This is used in the prometheus metric file as `{labelname1="labelvalue1", labelname2="labelvalue2"}`. Labels can have a static value by specifying the Value or a reference to a tagname which has the value.

Tag with a label that has a static value and another label with a dynamic value:
```yaml
- Name: MeterStatus1
OffsetByte: 1004
Type: String
StringLength: 254
- MetricsName: mytags_power
Labels:
- Name: meter
    Value: Appartment1
- Name: status
    ValueTagName: MeterStatus1
# Result: mytags_power{meter="Appartment1", status="Status read from PLC"}
```

### Types
- `Bool` (Bit, `OffsetBit` must be supplied (default 0))
- `Int8` (Byte)
- `Int16` (Int)
- `Int32` (DInt)
- `Int64` (LInt)
- `Float` (Real)
- `Double` (LReal)
- `String` (`StringLength` must be supplied, this is the number of characters, not the byte length)

A string cannot have a MetricsName as a string cannot be read by Prometheus, only as a Label.

## Acknowledgements
Uses the Snap7 library available on http://snap7.sourceforge.net/
