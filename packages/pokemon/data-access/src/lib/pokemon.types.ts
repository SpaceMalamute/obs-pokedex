export const POKEMON_TYPES = [
  "normal",
  "fire",
  "water",
  "electric",
  "grass",
  "ice",
  "fighting",
  "poison",
  "ground",
  "flying",
  "psychic",
  "bug",
  "rock",
  "ghost",
  "dragon",
  "dark",
  "steel",
  "fairy",
] as const;

export type PokemonType = (typeof POKEMON_TYPES)[number];

export interface Pokemon {
  readonly id: string;
  readonly pokedexNumber: number;
  readonly name: string;
  readonly primaryType: PokemonType;
  readonly secondaryType: PokemonType | null;
  readonly generation: number;
  readonly hp: number;
  readonly attack: number;
  readonly defense: number;
  readonly specialAttack: number;
  readonly specialDefense: number;
  readonly speed: number;
  readonly spriteUrl: string;
  readonly description: string;
  readonly createdAt: string;
  readonly updatedAt: string;
}

export interface PokemonListParams {
  readonly page?: number;
  readonly pageSize?: number;
  readonly search?: string;
  readonly type?: PokemonType;
}

export interface PaginatedResponse<T> {
  readonly items: readonly T[];
  readonly total: number;
  readonly page: number;
  readonly pageSize: number;
}
