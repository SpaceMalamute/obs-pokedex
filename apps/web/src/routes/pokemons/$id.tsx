import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/pokemons/$id')({
	component: PokemonDetailPage,
})

function PokemonDetailPage() {
	const { id } = Route.useParams()
	return (
		<div>
			<h1>Pokemon {id}</h1>
		</div>
	)
}
