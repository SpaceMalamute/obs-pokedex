import { createFileRoute } from '@tanstack/react-router'
import { z } from 'zod'

const pokemonSearchSchema = z.object({
	page: z.number().optional().default(1),
	pageSize: z.number().optional().default(20),
	search: z.string().optional(),
	type: z.string().optional(),
})

export const Route = createFileRoute('/pokemons/')({
	component: PokemonListPage,
	validateSearch: pokemonSearchSchema,
})

function PokemonListPage() {
	const { page, pageSize, search, type } = Route.useSearch()
	return (
		<div>
			<h1>Pokemon List</h1>
			<p>
				Page {page}, size {pageSize}
			</p>
		</div>
	)
}
