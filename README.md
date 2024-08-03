# Nitou Unity Lib

## 【概要】
基本的な拡張メソッドやMath関連の汎用クラス（構造体）を備えたライブラリ．


## test


@startuml
skinparam classAttributeIconSize 0

title サンプルクラス

class Encoder{
    + translate()
}
Interface Reader
Reader <|-- CsvReader
Encoder .> Reader
@enduml
@enduml