import axios from "axios";
import type {
  PaginatedResponse,
  Pokemon,
  PokemonListParams,
} from "./pokemon.types";
import {
  getCreatePokemonUrl,
  getDeletePokemonUrl,
  getPokemonByIdUrl,
  getPokemonListUrl,
  getUpdatePokemonUrl,
} from "./pokemon.urls";

const api = axios.create({
  baseURL: "http://localhost:5276",
});

export async function getPokemonList(
  params?: PokemonListParams,
): Promise<PaginatedResponse<Pokemon>> {
  const { data } = await api.get<PaginatedResponse<Pokemon>>(
    getPokemonListUrl(params),
  );
  return data;
}

export async function getPokemonById(id: string): Promise<Pokemon> {
  const { data } = await api.get<Pokemon>(getPokemonByIdUrl(id));
  return data;
}

export type CreatePokemonPayload = Omit<
  Pokemon,
  "id" | "createdAt" | "updatedAt"
>;

export async function createPokemon(
  payload: CreatePokemonPayload,
): Promise<Pokemon> {
  const { data } = await api.post<Pokemon>(getCreatePokemonUrl(), payload);
  return data;
}

export type UpdatePokemonPayload = CreatePokemonPayload;

export async function updatePokemon(
  id: string,
  payload: UpdatePokemonPayload,
): Promise<Pokemon> {
  const { data } = await api.put<Pokemon>(getUpdatePokemonUrl(id), payload);
  return data;
}

export async function deletePokemon(id: string): Promise<void> {
  await api.delete(getDeletePokemonUrl(id));
}
