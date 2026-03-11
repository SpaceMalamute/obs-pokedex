import { apiClient } from '@obs-pokedex/shared-data-access'
import type {
	PaginatedResponse,
	Pokemon,
	PokemonListParams,
} from './pokemon.types.ts'
import {
	getCreatePokemonUrl,
	getDeletePokemonUrl,
	getPokemonByIdUrl,
	getPokemonListUrl,
	getUpdatePokemonUrl,
} from './pokemon.urls.ts'

export async function getPokemonList(
	params?: PokemonListParams,
): Promise<PaginatedResponse<Pokemon>> {
	const { data } = await apiClient.get<PaginatedResponse<Pokemon>>(
		getPokemonListUrl(params),
	)
	return data
}

export async function getPokemonById(id: string): Promise<Pokemon> {
	const { data } = await apiClient.get<Pokemon>(getPokemonByIdUrl(id))
	return data
}

export type CreatePokemonPayload = Omit<
	Pokemon,
	'id' | 'createdAt' | 'updatedAt'
>

export async function createPokemon(
	payload: CreatePokemonPayload,
): Promise<Pokemon> {
	const { data } = await apiClient.post<Pokemon>(getCreatePokemonUrl(), payload)
	return data
}

export type UpdatePokemonPayload = CreatePokemonPayload

export async function updatePokemon(
	id: string,
	payload: UpdatePokemonPayload,
): Promise<Pokemon> {
	const { data } = await apiClient.put<Pokemon>(
		getUpdatePokemonUrl(id),
		payload,
	)
	return data
}

export async function deletePokemon(id: string): Promise<void> {
	await apiClient.delete(getDeletePokemonUrl(id))
}
