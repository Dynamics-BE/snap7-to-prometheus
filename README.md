# snap7-to-prometheus
Getting PLC tags out of S7-1200/1500 and presenting them as metrics that can be read by prometheus

Metrics are available on http://localhost:5489/metrics

## Config file
Config file is reloaded automatically upon changes. (This doesn't work if config file is supplies via a docker volume.)

### Filetypes
- 'Bool' (Bit, 'OffsetBit' must be supplied ('default 0'))
- 'Int8' (Byte)
- 'Int16' (Int)
- 'Int32' (DInt)
- 'Int64' (LInt)
- 'Float' (Real)
- 'Double' (LReal)
- 'String' ('StringLength' must be supplied, this is the number of characters, not the byte length)