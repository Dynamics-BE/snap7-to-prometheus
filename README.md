# snap7-to-prometheus
Getting PLC tags out of Siemens PLCs and presenting them as a plaintext metrics file that can be read by prometheus.

## Building and running the appliction
* copy `example.docker-compose.yml` to `docker-compose.yml` and adjust for your purposes
* copy `./snap7-to-prometheus/config.yml` to `./config.yml` fill in your tags
* execute `sudo docker-compose up --build`

Configure prometheus to use the metrics file that is available on http://`host`:5489/metrics

## Config file
The config files has connection details and a list of tags that will be exposed in the metrics file
Config file is reloaded automatically upon changes. (This doesn't work if config file is supplies via a docker volume.)

### Types
- `Bool` (Bit, `OffsetBit` must be supplied (default 0))
- `Int8` (Byte)
- `Int16` (Int)
- `Int32` (DInt)
- `Int64` (LInt)
- `Float` (Real)
- `Double` (LReal)
- `String` (`StringLength` must be supplied, this is the number of characters, not the byte length)

## Acknowledgements
Uses the Snap7 library available on http://snap7.sourceforge.net/
