import { RouterProvider, createRouter } from '@tanstack/react-router'
import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { routeTree } from './routeTree.gen'

const router = createRouter({
	routeTree,
	defaultPreload: 'intent',
})

declare module '@tanstack/react-router' {
	interface Register {
		router: typeof router
	}
}

const root = createRoot(document.getElementById('root') as HTMLElement)

root.render(
	<StrictMode>
		<RouterProvider router={router} />
	</StrictMode>,
)
