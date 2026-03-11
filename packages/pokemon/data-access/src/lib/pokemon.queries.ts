import { queryOptions } from "@tanstack/react-query";
import { getPokemonById, getPokemonList } from "./pokemon.api";
import type { PokemonListParams } from "./pokemon.types";

export const pokemonKeys = {
  all: ["pokemons"] as const,
  lists: () => [...pokemonKeys.all, "list"] as const,
  list: (params?: PokemonListParams) =>
    [...pokemonKeys.lists(), params] as const,
  details: () => [...pokemonKeys.all, "detail"] as const,
  detail: (id: string) => [...pokemonKeys.details(), id] as const,
};

export function pokemonListQueryOptions(params?: PokemonListParams) {
  return queryOptions({
    queryKey: pokemonKeys.list(params),
    queryFn: () => getPokemonList(params),
  });
}

export function pokemonDetailQueryOptions(id: string) {
  return queryOptions({
    queryKey: pokemonKeys.detail(id),
    queryFn: () => getPokemonById(id),
  });
}
