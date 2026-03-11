// biome-ignore lint/style/noRestrictedImports: this is the single custom axios instance factory
import axios from 'axios'

export const apiClient = axios.create({
	baseURL: 'http://localhost:5276',
})
