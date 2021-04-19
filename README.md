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
