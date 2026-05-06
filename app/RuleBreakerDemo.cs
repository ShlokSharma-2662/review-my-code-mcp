using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace company.product_demo.bad_namespace
{
    public interface ReviewService
    {
        void Run();
    }

    public record DemoRecord(int Id, string Name);

    public class DemoEntity
    {
        public int Id { get; set; }
    }

    public class AnotherEntity
    {
        public string Value { get; set; } = string.Empty;
    }

    public class RuleBreakerProgram
    {
        public static void Main(string[] args)
        {
            var program = new RuleBreakerProgram();
            program.fire_and_forget();
            program.bad_method_name(1, 2, 3, 4, 5, 6);
            program.DangerousSql("userInput");
        }

        public async void fire_and_forget()
        {
            var task = Task.Run(() => 42);
            var value = task.Result;
            Console.WriteLine(value);
            Task.Delay(5).Wait();
        }

        public void bad_method_name(int a, int b, int c, int d, int e, int f)
        {
            Console.WriteLine(a + b + c + d + e + f);
        }

        public void DangerousSql(string userInput)
        {
            var db = new FakeDbContext();

            var sql = "SELECT " + userInput;
            var password = "P@ssword123!";
            var secret = "super-secret";
            var apikey = "my-demo-key";

            var items = new List<int> { 1, 2, 3, 4 };
            var countA = items.ToList().Count;
            if (items.Count() > 0)
            {
                Console.WriteLine("Has data");
            }

            var repeated = new string('x', 12);
            Console.WriteLine(repeated);

            Process.Start(userInput);
            var raw = db.Users.FromSqlRaw("SELECT * FROM Users WHERE Name = '" + userInput + "'");
            Console.WriteLine(raw.Count());

            try
            {
                ThrowSomething();
            }
            catch (Exception)
            {
                Console.WriteLine(sql);
            }

            try
            {
                ThrowSomething();
            }
            catch {
            }
        }

        private static void ThrowSomething()
        {
            throw new InvalidOperationException("Demo exception");
        }
    }

    public class FakeDbContext
    {
        public FakeUserSet Users { get; } = new();
    }

    public class FakeUserSet
    {
        public IEnumerable<string> FromSqlRaw(string sql)
        {
            return Array.Empty<string>();
        }
    }
}
// filler line 1 for large-file rule
// filler line 2 for large-file rule
// filler line 3 for large-file rule
// filler line 4 for large-file rule
// filler line 5 for large-file rule
// filler line 6 for large-file rule
// filler line 7 for large-file rule
// filler line 8 for large-file rule
// filler line 9 for large-file rule
// filler line 10 for large-file rule
// filler line 11 for large-file rule
// filler line 12 for large-file rule
// filler line 13 for large-file rule
// filler line 14 for large-file rule
// filler line 15 for large-file rule
// filler line 16 for large-file rule
// filler line 17 for large-file rule
// filler line 18 for large-file rule
// filler line 19 for large-file rule
// filler line 20 for large-file rule
// filler line 21 for large-file rule
// filler line 22 for large-file rule
// filler line 23 for large-file rule
// filler line 24 for large-file rule
// filler line 25 for large-file rule
// filler line 26 for large-file rule
// filler line 27 for large-file rule
// filler line 28 for large-file rule
// filler line 29 for large-file rule
// filler line 30 for large-file rule
// filler line 31 for large-file rule
// filler line 32 for large-file rule
// filler line 33 for large-file rule
// filler line 34 for large-file rule
// filler line 35 for large-file rule
// filler line 36 for large-file rule
// filler line 37 for large-file rule
// filler line 38 for large-file rule
// filler line 39 for large-file rule
// filler line 40 for large-file rule
// filler line 41 for large-file rule
// filler line 42 for large-file rule
// filler line 43 for large-file rule
// filler line 44 for large-file rule
// filler line 45 for large-file rule
// filler line 46 for large-file rule
// filler line 47 for large-file rule
// filler line 48 for large-file rule
// filler line 49 for large-file rule
// filler line 50 for large-file rule
// filler line 51 for large-file rule
// filler line 52 for large-file rule
// filler line 53 for large-file rule
// filler line 54 for large-file rule
// filler line 55 for large-file rule
// filler line 56 for large-file rule
// filler line 57 for large-file rule
// filler line 58 for large-file rule
// filler line 59 for large-file rule
// filler line 60 for large-file rule
// filler line 61 for large-file rule
// filler line 62 for large-file rule
// filler line 63 for large-file rule
// filler line 64 for large-file rule
// filler line 65 for large-file rule
// filler line 66 for large-file rule
// filler line 67 for large-file rule
// filler line 68 for large-file rule
// filler line 69 for large-file rule
// filler line 70 for large-file rule
// filler line 71 for large-file rule
// filler line 72 for large-file rule
// filler line 73 for large-file rule
// filler line 74 for large-file rule
// filler line 75 for large-file rule
// filler line 76 for large-file rule
// filler line 77 for large-file rule
// filler line 78 for large-file rule
// filler line 79 for large-file rule
// filler line 80 for large-file rule
// filler line 81 for large-file rule
// filler line 82 for large-file rule
// filler line 83 for large-file rule
// filler line 84 for large-file rule
// filler line 85 for large-file rule
// filler line 86 for large-file rule
// filler line 87 for large-file rule
// filler line 88 for large-file rule
// filler line 89 for large-file rule
// filler line 90 for large-file rule
// filler line 91 for large-file rule
// filler line 92 for large-file rule
// filler line 93 for large-file rule
// filler line 94 for large-file rule
// filler line 95 for large-file rule
// filler line 96 for large-file rule
// filler line 97 for large-file rule
// filler line 98 for large-file rule
// filler line 99 for large-file rule
// filler line 100 for large-file rule
// filler line 101 for large-file rule
// filler line 102 for large-file rule
// filler line 103 for large-file rule
// filler line 104 for large-file rule
// filler line 105 for large-file rule
// filler line 106 for large-file rule
// filler line 107 for large-file rule
// filler line 108 for large-file rule
// filler line 109 for large-file rule
// filler line 110 for large-file rule
// filler line 111 for large-file rule
// filler line 112 for large-file rule
// filler line 113 for large-file rule
// filler line 114 for large-file rule
// filler line 115 for large-file rule
// filler line 116 for large-file rule
// filler line 117 for large-file rule
// filler line 118 for large-file rule
// filler line 119 for large-file rule
// filler line 120 for large-file rule
// filler line 121 for large-file rule
// filler line 122 for large-file rule
// filler line 123 for large-file rule
// filler line 124 for large-file rule
// filler line 125 for large-file rule
// filler line 126 for large-file rule
// filler line 127 for large-file rule
// filler line 128 for large-file rule
// filler line 129 for large-file rule
// filler line 130 for large-file rule
// filler line 131 for large-file rule
// filler line 132 for large-file rule
// filler line 133 for large-file rule
// filler line 134 for large-file rule
// filler line 135 for large-file rule
// filler line 136 for large-file rule
// filler line 137 for large-file rule
// filler line 138 for large-file rule
// filler line 139 for large-file rule
// filler line 140 for large-file rule
// filler line 141 for large-file rule
// filler line 142 for large-file rule
// filler line 143 for large-file rule
// filler line 144 for large-file rule
// filler line 145 for large-file rule
// filler line 146 for large-file rule
// filler line 147 for large-file rule
// filler line 148 for large-file rule
// filler line 149 for large-file rule
// filler line 150 for large-file rule
// filler line 151 for large-file rule
// filler line 152 for large-file rule
// filler line 153 for large-file rule
// filler line 154 for large-file rule
// filler line 155 for large-file rule
// filler line 156 for large-file rule
// filler line 157 for large-file rule
// filler line 158 for large-file rule
// filler line 159 for large-file rule
// filler line 160 for large-file rule
// filler line 161 for large-file rule
// filler line 162 for large-file rule
// filler line 163 for large-file rule
// filler line 164 for large-file rule
// filler line 165 for large-file rule
// filler line 166 for large-file rule
// filler line 167 for large-file rule
// filler line 168 for large-file rule
// filler line 169 for large-file rule
// filler line 170 for large-file rule
// filler line 171 for large-file rule
// filler line 172 for large-file rule
// filler line 173 for large-file rule
// filler line 174 for large-file rule
// filler line 175 for large-file rule
// filler line 176 for large-file rule
// filler line 177 for large-file rule
// filler line 178 for large-file rule
// filler line 179 for large-file rule
// filler line 180 for large-file rule
// filler line 181 for large-file rule
// filler line 182 for large-file rule
// filler line 183 for large-file rule
// filler line 184 for large-file rule
// filler line 185 for large-file rule
// filler line 186 for large-file rule
// filler line 187 for large-file rule
// filler line 188 for large-file rule
// filler line 189 for large-file rule
// filler line 190 for large-file rule
// filler line 191 for large-file rule
// filler line 192 for large-file rule
// filler line 193 for large-file rule
// filler line 194 for large-file rule
// filler line 195 for large-file rule
// filler line 196 for large-file rule
// filler line 197 for large-file rule
// filler line 198 for large-file rule
// filler line 199 for large-file rule
// filler line 200 for large-file rule
// filler line 201 for large-file rule
// filler line 202 for large-file rule
// filler line 203 for large-file rule
// filler line 204 for large-file rule
// filler line 205 for large-file rule
// filler line 206 for large-file rule
// filler line 207 for large-file rule
// filler line 208 for large-file rule
// filler line 209 for large-file rule
// filler line 210 for large-file rule
// filler line 211 for large-file rule
// filler line 212 for large-file rule
// filler line 213 for large-file rule
// filler line 214 for large-file rule
// filler line 215 for large-file rule
// filler line 216 for large-file rule
// filler line 217 for large-file rule
// filler line 218 for large-file rule
// filler line 219 for large-file rule
// filler line 220 for large-file rule
// filler line 221 for large-file rule
// filler line 222 for large-file rule
// filler line 223 for large-file rule
// filler line 224 for large-file rule
// filler line 225 for large-file rule
// filler line 226 for large-file rule
// filler line 227 for large-file rule
// filler line 228 for large-file rule
// filler line 229 for large-file rule
// filler line 230 for large-file rule
// filler line 231 for large-file rule
// filler line 232 for large-file rule
// filler line 233 for large-file rule
// filler line 234 for large-file rule
// filler line 235 for large-file rule
// filler line 236 for large-file rule
// filler line 237 for large-file rule
// filler line 238 for large-file rule
// filler line 239 for large-file rule
// filler line 240 for large-file rule
// filler line 241 for large-file rule
// filler line 242 for large-file rule
// filler line 243 for large-file rule
// filler line 244 for large-file rule
// filler line 245 for large-file rule
// filler line 246 for large-file rule
// filler line 247 for large-file rule
// filler line 248 for large-file rule
// filler line 249 for large-file rule
// filler line 250 for large-file rule
// filler line 251 for large-file rule
// filler line 252 for large-file rule
// filler line 253 for large-file rule
// filler line 254 for large-file rule
// filler line 255 for large-file rule
// filler line 256 for large-file rule
// filler line 257 for large-file rule
// filler line 258 for large-file rule
// filler line 259 for large-file rule
// filler line 260 for large-file rule
// filler line 261 for large-file rule
// filler line 262 for large-file rule
// filler line 263 for large-file rule
// filler line 264 for large-file rule
// filler line 265 for large-file rule
// filler line 266 for large-file rule
// filler line 267 for large-file rule
// filler line 268 for large-file rule
// filler line 269 for large-file rule
// filler line 270 for large-file rule
// filler line 271 for large-file rule
// filler line 272 for large-file rule
// filler line 273 for large-file rule
// filler line 274 for large-file rule
// filler line 275 for large-file rule
// filler line 276 for large-file rule
// filler line 277 for large-file rule
// filler line 278 for large-file rule
// filler line 279 for large-file rule
// filler line 280 for large-file rule
// filler line 281 for large-file rule
// filler line 282 for large-file rule
// filler line 283 for large-file rule
// filler line 284 for large-file rule
// filler line 285 for large-file rule
// filler line 286 for large-file rule
// filler line 287 for large-file rule
// filler line 288 for large-file rule
// filler line 289 for large-file rule
// filler line 290 for large-file rule
// filler line 291 for large-file rule
// filler line 292 for large-file rule
// filler line 293 for large-file rule
// filler line 294 for large-file rule
// filler line 295 for large-file rule
// filler line 296 for large-file rule
// filler line 297 for large-file rule
// filler line 298 for large-file rule
// filler line 299 for large-file rule
// filler line 300 for large-file rule
// filler line 301 for large-file rule
// filler line 302 for large-file rule
// filler line 303 for large-file rule
// filler line 304 for large-file rule
// filler line 305 for large-file rule
// filler line 306 for large-file rule
// filler line 307 for large-file rule
// filler line 308 for large-file rule
// filler line 309 for large-file rule
// filler line 310 for large-file rule
// filler line 311 for large-file rule
// filler line 312 for large-file rule
// filler line 313 for large-file rule
// filler line 314 for large-file rule
// filler line 315 for large-file rule
// filler line 316 for large-file rule
// filler line 317 for large-file rule
// filler line 318 for large-file rule
// filler line 319 for large-file rule
// filler line 320 for large-file rule
// filler line 321 for large-file rule
// filler line 322 for large-file rule
// filler line 323 for large-file rule
// filler line 324 for large-file rule
// filler line 325 for large-file rule
// filler line 326 for large-file rule
// filler line 327 for large-file rule
// filler line 328 for large-file rule
// filler line 329 for large-file rule
// filler line 330 for large-file rule
// filler line 331 for large-file rule
// filler line 332 for large-file rule
// filler line 333 for large-file rule
// filler line 334 for large-file rule
// filler line 335 for large-file rule
// filler line 336 for large-file rule
// filler line 337 for large-file rule
// filler line 338 for large-file rule
// filler line 339 for large-file rule
// filler line 340 for large-file rule
// filler line 341 for large-file rule
// filler line 342 for large-file rule
// filler line 343 for large-file rule
// filler line 344 for large-file rule
// filler line 345 for large-file rule
// filler line 346 for large-file rule
// filler line 347 for large-file rule
// filler line 348 for large-file rule
// filler line 349 for large-file rule
// filler line 350 for large-file rule
// filler line 351 for large-file rule
// filler line 352 for large-file rule
// filler line 353 for large-file rule
// filler line 354 for large-file rule
// filler line 355 for large-file rule
// filler line 356 for large-file rule
// filler line 357 for large-file rule
// filler line 358 for large-file rule
// filler line 359 for large-file rule
// filler line 360 for large-file rule
// filler line 361 for large-file rule
// filler line 362 for large-file rule
// filler line 363 for large-file rule
// filler line 364 for large-file rule
// filler line 365 for large-file rule
// filler line 366 for large-file rule
// filler line 367 for large-file rule
// filler line 368 for large-file rule
// filler line 369 for large-file rule
// filler line 370 for large-file rule
// filler line 371 for large-file rule
// filler line 372 for large-file rule
// filler line 373 for large-file rule
// filler line 374 for large-file rule
// filler line 375 for large-file rule
// filler line 376 for large-file rule
// filler line 377 for large-file rule
// filler line 378 for large-file rule
// filler line 379 for large-file rule
// filler line 380 for large-file rule
// filler line 381 for large-file rule
// filler line 382 for large-file rule
// filler line 383 for large-file rule
// filler line 384 for large-file rule
// filler line 385 for large-file rule
// filler line 386 for large-file rule
// filler line 387 for large-file rule
// filler line 388 for large-file rule
// filler line 389 for large-file rule
// filler line 390 for large-file rule
// filler line 391 for large-file rule
// filler line 392 for large-file rule
// filler line 393 for large-file rule
// filler line 394 for large-file rule
// filler line 395 for large-file rule
// filler line 396 for large-file rule
// filler line 397 for large-file rule
// filler line 398 for large-file rule
// filler line 399 for large-file rule
// filler line 400 for large-file rule
// filler line 401 for large-file rule
// filler line 402 for large-file rule
// filler line 403 for large-file rule
// filler line 404 for large-file rule
// filler line 405 for large-file rule
// filler line 406 for large-file rule
// filler line 407 for large-file rule
// filler line 408 for large-file rule
// filler line 409 for large-file rule
// filler line 410 for large-file rule
// filler line 411 for large-file rule
// filler line 412 for large-file rule
// filler line 413 for large-file rule
// filler line 414 for large-file rule
// filler line 415 for large-file rule
// filler line 416 for large-file rule
// filler line 417 for large-file rule
// filler line 418 for large-file rule
// filler line 419 for large-file rule
// filler line 420 for large-file rule
