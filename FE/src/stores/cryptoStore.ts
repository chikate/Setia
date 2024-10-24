// Standard Data Structure for Ticker Price
export interface IStandardTickerPrice {
  name: string
  price: number
}
export interface IStandardCoinData {
  id: string
  rank: string
  symbol: string
  name: string
  supply: string
  maxSupply?: string
  marketCapUsd: string
  volumeUsd24Hr: string
  priceUsd: string
  changePercent24Hr: string
  vwap24Hr: string
  explorer?: string
}

export const cryptoStore = defineStore('cryptoStore', () => {
  // CoinCap's Data Structure for Ticker Price
  interface ICoinCapCoinData {
    id: string
    rank: string
    symbol: string
    name: string
    supply: string
    maxSupply?: string
    marketCapUsd: string
    volumeUsd24Hr: string
    priceUsd: string
    changePercent24Hr: string
    vwap24Hr: string
    explorer?: string
  }
  interface ICoinCapTickerPrice {
    data: ICoinCapCoinData[]
    timestamp: number
  }

  const getCoinCapCoin = (symbolFilter?: string): Promise<IStandardCoinData[]> =>
    fetch(`https://api.coincap.io/v2/assets`)
      .then((response: Response) => response.json())
      .then((elem: ICoinCapTickerPrice) =>
        symbolFilter
          ? elem.data.filter((coin: ICoinCapCoinData) => coin.symbol == symbolFilter)
          : elem.data
      )

  return {
    getCoinCapCoin
  }
})
