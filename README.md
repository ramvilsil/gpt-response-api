# GPT Response API Documentation

## Endpoints

Base URL: `localhost/api/`

### POST /main

#### Request Body
```json
{
	"content": "What is gpt?"
}
```

#### Response Body
```json
{
	"role": "assistant",
	"content": "GPT stands for \"Generative Pre-trained Transformer.\" It is a type of artificial intelligence model that uses deep learning techniques to generate human-like text. GPT models are trained on large amounts of text data and can be used for various natural language processing tasks, such as language translation, text completion, question answering, and more."
}
```

### POST /main/conversation

#### Request Body
```json
{
	"system": "You are a funny assistant",
	"messages": [
		{
			"role": "user",
			"content": "What is gpt?"
		},
		{
			"role": "assistant",
			"content": "GPT stands for \"Generative Pre-trained Transformer.\" It is a type of artificial intelligence model that uses deep learning techniques to generate human-like text. GPT models are trained on large amounts of text data and can be used for various natural language processing tasks, such as language translation, text completion, question answering, and more."
		},
		{
			"role": "user",
			"content": "I don't understand"
		}
	]
}
```

#### Response Body
```json
{
	"role": "assistant",
	"content": "Well, think of GPT as a super-smart language model. It's like having a really witty and knowledgeable friend who can generate text that sounds just like a human wrote it. GPT has been trained on tons of text from the internet, books, and other sources, so it has a vast knowledge base to draw from. It can understand and generate text in a way that's pretty impressive for a machine. So, in a nutshell, GPT is a fancy AI model that can generate human-like text. Hope that clears things up a bit!"
}
```