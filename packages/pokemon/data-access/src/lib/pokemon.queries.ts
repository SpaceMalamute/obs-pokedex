import { queryOptions } from '@tanstack/react-query'
import { getPokemonById, getPokemonList } from './pokemon.api.ts'
import type { PokemonListParams } from './pokemon.types.ts'

export const pokemonKeys = {
	all: ['pokemons'] as const,
	detail: (id: string) => [...pokemonKeys.details(), id] as const,
	details: () => [...pokemonKeys.all, 'detail'] as const,
	list: (params?: PokemonListParams) =>
		[...pokemonKeys.lists(), params] as const,
	lists: () => [...pokemonKeys.all, 'list'] as const,
}

export function pokemonListQueryOptions(params?: PokemonListParams) {
	return queryOptions({
		queryFn: () => getPokemonList(params),
		queryKey: pokemonKeys.list(params),
	})
}

export function pokemonDetailQueryOptions(id: string) {
	return queryOptions({
		queryFn: () => getPokemonById(id),
		queryKey: pokemonKeys.detail(id),
	})
}
