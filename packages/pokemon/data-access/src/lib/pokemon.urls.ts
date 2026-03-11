import type { PokemonListParams } from "./pokemon.types";

const BASE_PATH = "/api/v1/pokemons";

export function getPokemonListUrl(params?: PokemonListParams): string {
  const searchParams = new URLSearchParams();

  if (params?.page) searchParams.set("page", String(params.page));
  if (params?.pageSize) searchParams.set("pageSize", String(params.pageSize));
  if (params?.search) searchParams.set("search", params.search);
  if (params?.type) searchParams.set("type", params.type);

  const query = searchParams.toString();
  return query ? `${BASE_PATH}?${query}` : BASE_PATH;
}

export function getPokemonByIdUrl(id: string): string {
  return `${BASE_PATH}/${id}`;
}

export function getCreatePokemonUrl(): string {
  return BASE_PATH;
}

export function getUpdatePokemonUrl(id: string): string {
  return `${BASE_PATH}/${id}`;
}

export function getDeletePokemonUrl(id: string): string {
  return `${BASE_PATH}/${id}`;
}
