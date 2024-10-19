import { apiRequest } from '@/helpers'

// Standard Data Structure for Ticker Price
export interface IStandardTickerPrice {
  name: string
  price: number
}

// Binance's Data Structure for Ticker Price
export interface IBinanceTickerPrice {
  symbol: string // 'BTCUSDT'
  price: string // '50000.00' - Binance returns price as a string
}

const API_CALL = 'https://api.binance.com/api/v3/ticker/price'

export const cryptoStore = defineStore('cryptoStore', () => {
  const BINANCE_API_KEY = 'put your own API Key here'

  const getCoin = async (symbol: string): Promise<IStandardTickerPrice> =>
    await (
      await apiRequest(`${API_CALL}`, { symbol })
    ).map(
      (BinanceTickerPrice: IBinanceTickerPrice) =>
        ({
          name: BinanceTickerPrice.symbol,
          symbol: BinanceTickerPrice.symbol,
          asset: BinanceTickerPrice.symbol,
          price: parseFloat(BinanceTickerPrice.price)
        }) as IStandardTickerPrice
    )

  return {
    getCoin
  }
})
